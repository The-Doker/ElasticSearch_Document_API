using ElasticSearch_gRPC_Service;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Implementation
{
    public class gRpcDocumentSaver : IDocumentSaver
    {
        private readonly DocumentHelper.DocumentHelperClient _client;

        public gRpcDocumentSaver(DocumentHelper.DocumentHelperClient client)
        {
            _client = client;
        }

        public async Task<bool> SaveBase64Document(string doc)
        {
            var replyFromGrpc = await _client.UploadFileToElasticAsync(new UploadFileRequest { Base64Data = doc });
            return replyFromGrpc.Result;
        }
    }
}
