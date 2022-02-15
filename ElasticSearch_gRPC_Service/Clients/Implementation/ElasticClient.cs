using ElasticSearch_gRPC_Service.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch_gRPC_Service.Commons
{
    public class ElasticClient : IElasticClient
    {
        private readonly ElasticWebSettings _elasticWebSettings;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ElasticClient> _logger;

        public ElasticClient(IHttpClientFactory httpClientFactory, IOptions<ElasticWebSettings> settingsOptions, ILogger<ElasticClient> logger)
        {
            _elasticWebSettings = settingsOptions.Value;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<HttpResponseMessage> SendSearchRequestToElasticAsync(string searchQuery)
        {
            _logger.LogInformation("Получен запрос на выполнение поиска в ElasticSearch");
            var httpClient = _httpClientFactory.CreateClient();
            string url = _elasticWebSettings.ElasticAddress + "_search";
            string body = @"{""query"":{""multi_match"":{""query"":""" +
                searchQuery
                + @""",""fields"":[""attachment.content"",""attachment.author"",""attachment.title""]}},""_source"":{""excludes"": [""attachment.content""]},""highlight"":{""fields"":{""attachment.content"":{""number_of_fragments"":10,""fragment_size"":300}}}}";

            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            _logger.LogInformation("Получен ответ от ElasticSearch");
            return response;
        }

        public async Task<HttpResponseMessage> SendFileToElasticAsync(string dataInBase64)
        {
            _logger.LogInformation("Получен запрос на отправку файла в ElasticSearch");
            var httpClient = _httpClientFactory.CreateClient();
            string url = _elasticWebSettings.ElasticAddress + "_doc?pipeline=attachment";
            string body = "{ \"data\": \"" + dataInBase64 + "\" }";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(url, content);
            _logger.LogInformation("Получен ответ от ElasticSearch");
            return result;
        }

        public async Task<HttpResponseMessage> SendDownloadRequestToElasticAsync(string searchId)
        {
            _logger.LogInformation("Получен запрос на загрузку файла из ElasticSearch");
            var httpClient = _httpClientFactory.CreateClient();
            string url = _elasticWebSettings.ElasticAddress + "_search";
            string body = @"{""query"":{""term"":{""_id"":""" +
                searchId +
                @"""}},""_source"":{""excludes"":[""attachment.content""]}}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            _logger.LogInformation("Получен ответ от ElasticSearch");
            return response;
        }
    }
}
