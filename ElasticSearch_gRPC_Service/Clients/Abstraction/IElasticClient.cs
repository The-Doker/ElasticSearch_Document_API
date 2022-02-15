using System.Net.Http;
using System.Threading.Tasks;

namespace ElasticSearch_gRPC_Service.Commons
{
    public interface IElasticClient
    {
        Task<HttpResponseMessage> SendDownloadRequestToElasticAsync(string searchId);
        Task<HttpResponseMessage> SendFileToElasticAsync(string dataInBase64);
        Task<HttpResponseMessage> SendSearchRequestToElasticAsync(string searchQuery);
    }
}