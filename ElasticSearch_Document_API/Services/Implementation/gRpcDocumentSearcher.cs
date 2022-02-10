using ElasticSearch_Document_API.Services.Abstraction;
using ElasticSearch_gRPC_Service;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Implementation
{
    public class gRpcDocumentSearcher : IDocumentSearcher
    {
        public async Task<string> FindInSavedDocuments(string searchQuery)
        {
            using var channel = GrpcChannel.ForAddress("http://elasticgrpc:5000");
            var grpcClient = new DocumentHelper.DocumentHelperClient(channel);
            var replyFromGrpc = await grpcClient.FindFileInElasticAsync(new FileSearchRequest { Query = searchQuery });
            return replyFromGrpc.JsonData;
        }
    }
}
