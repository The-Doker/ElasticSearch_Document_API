using System;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
namespace ElasticSearch_gRPC_Service.Interceptors
{
    public class GlobalExceptionsInterceptor : Interceptor
    {
        private readonly ILogger<GlobalExceptionsInterceptor> _logger;

        public GlobalExceptionsInterceptor(ILogger<GlobalExceptionsInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                _logger.LogInformation($"{Environment.NewLine}GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}" +
                    $"Data: {JsonSerializer.Serialize(request)}");
                var serverResponse = await continuation(request, context);
                _logger.LogInformation($"{Environment.NewLine}GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}" +
                    $"Data: {JsonSerializer.Serialize(serverResponse)}");
                return serverResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown by {context.Method}.");
                throw;
            }
        }
    }
}
