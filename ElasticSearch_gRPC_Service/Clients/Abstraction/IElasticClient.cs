using System.Net.Http;

namespace ElasticSearch_gRPC_Service.Commons
{
    public interface IElasticClient
    {
        HttpResponseMessage SendDownloadRequestToElastic(string searchId);
        void SendFileToElastic(string dataInBase64);
        HttpResponseMessage SendSearchRequestToElastic(string searchQuery);
    }
}