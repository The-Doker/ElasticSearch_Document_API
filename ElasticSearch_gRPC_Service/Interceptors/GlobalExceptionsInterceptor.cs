using System;
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
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown by {context.Method}.");
                throw;
            }
        }
    }
}
