using ElasticSearch_Document_API.Models;
using ElasticSearch_Document_API.Services.Abstraction;
using ElasticSearch_gRPC_Service;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Implementation
{
    public class gRpcDocumentGiver : IDocumentGiver
    {
        private readonly DocumentHelper.DocumentHelperClient _client;

        public gRpcDocumentGiver(DocumentHelper.DocumentHelperClient client)
        {
            _client = client;
        }

        public async Task<FileModel> GetDocumentFromSavedFiles(string documentId)
        {
            var replyFromGrpc = await _client.DownloadFileFromElasticAsync(new FileDownloadRequest { DocumentId = documentId });
            return new FileModel()
            {
                DataBase64 = replyFromGrpc.DocumentBase64,
                Name = replyFromGrpc.DocumentName,
                Type = replyFromGrpc.DocumentType
            };
        }
    }
}
