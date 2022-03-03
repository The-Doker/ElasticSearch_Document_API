using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                StreamReader requestStreamReader = null;
                StreamReader responseStreamReader = null;
                context.Request.EnableBuffering();

                var requestBodyStream = context.Request.Body;
                var requestBody = string.Empty;
                
                if (requestBodyStream != null)
                {
                    requestStreamReader = new StreamReader(requestBodyStream);
                    requestBody = await requestStreamReader.ReadToEndAsync();
                }
                    
                _logger.LogInformation("Request {method} {url}{queryString} \n" +
                    "Body: {requestBody}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request?.QueryString,
                    requestBody);

                if (requestBodyStream.CanSeek)
                    requestBodyStream.Seek(0, SeekOrigin.Begin);

                var originalBodyStream = context.Response.Body;
                var ms = new MemoryStream();
                context.Response.Body = ms;

                await _next.Invoke(context);

                var responseBodyStream = context.Response.Body;
                var responseBody = string.Empty;

                if (responseBodyStream != null)
                {
                    if (responseBodyStream.CanSeek)
                        responseBodyStream.Seek(0, SeekOrigin.Begin);

                    responseStreamReader = new StreamReader(responseBodyStream);
                    responseBody = await responseStreamReader.ReadToEndAsync();

                    if (responseBodyStream.CanSeek)
                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                }

                _logger.LogInformation("Response status code: {statusCode} \n" +
                    "Body: {responseBody}",
                    context.Response?.StatusCode,
                    responseBody);

                await ms.CopyToAsync(originalBodyStream);

                if (requestStreamReader != null)
                    requestStreamReader.Dispose();
                if (responseStreamReader != null)
                    responseStreamReader.Dispose();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(exception.Message);
        }
    }
}