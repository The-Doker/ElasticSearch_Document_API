using ElasticSearch_Document_API.Models;
using ElasticSearch_Document_API.Services.Abstraction;
using ElasticSearch_gRPC_Service;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Implementation
{
    public class gRpcDocumentGiver : IDocumentGiver
    {
        public async Task<FileModel> GetDocumentFromSavedFiles(string documentId)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var grpcClient = new DocumentHelper.DocumentHelperClient(channel);
            var replyFromGrpc = await grpcClient.DownloadFileFromElasticAsync(new FileDownloadRequest { DocumentId = documentId });
            return new FileModel()
            {
                DataBase64 = replyFromGrpc.DocumentBase64,
                Name = replyFromGrpc.DocumentName,
                Type = replyFromGrpc.DocumentType
            };
        }
    }
}
