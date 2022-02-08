using ElasticSearch_gRPC_Service;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Implementation
{
    public class gRpcDocumentSaver : IDocumentSaver
    {
        public async Task<bool> SaveBase64Document(string doc)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var grpcClient = new DocumentHelper.DocumentHelperClient(channel);
            var replyFromGrpc = await grpcClient.UploadFileToElasticAsync(new UploadFileRequest { Base64Data = doc });
            return replyFromGrpc.Result;
        }
    }
}
