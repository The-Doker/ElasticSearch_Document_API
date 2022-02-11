using ElasticSearch_Document_API.Services.Abstraction;
using ElasticSearch_gRPC_Service;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Implementation
{
    public class gRpcDocumentSearcher : IDocumentSearcher
    {
        private readonly DocumentHelper.DocumentHelperClient _client;
        
        public gRpcDocumentSearcher(DocumentHelper.DocumentHelperClient client)
        {
            _client = client;
        }

        public async Task<string> FindInSavedDocuments(string searchQuery)
        {
            var replyFromGrpc = await _client.FindFileInElasticAsync(new FileSearchRequest { Query = searchQuery });
            return replyFromGrpc.JsonData;
        }
    }
}
