using System.Net.Http;
using System.Text;

namespace ElasticSearch_gRPC_Service.Commons
{
    public static class HttpRequestSender
    {
        private static readonly string _url = "http://elasticsearchplugin:9200/documenthelper/";
        public static HttpResponseMessage SendSearchRequestToElastic(string searchQuery)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {

                string url = _url + "_search";
                string body = @"{""query"":{""multi_match"":{""query"":""" +
                    searchQuery
                    + @""",""fields"":[""attachment.content"",""attachment.author"",""attachment.title""]}},""_source"":{""excludes"": [""attachment.content""]},""highlight"":{""fields"":{""attachment.content"":{""number_of_fragments"":10,""fragment_size"":300}}}}";

                var content = new StringContent(body, Encoding.UTF8, "application/json");
                response = httpClient.PostAsync(url, content).Result;
            }
            return response;
        }

        public static void SendFileToElastic(string dataInBase64)
        {
            using (var httpClient = new HttpClient())
            {
                string url = _url + "_doc?pipeline=attachment";
                string body = "{ \"data\": \"" + dataInBase64 + "\" }";
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(url, content).Result;
            }
        }

        public static HttpResponseMessage SendDownloadRequestToElastic(string searchId)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {

                string url = _url + "_search";
                string body = @"{""query"":{""term"":{""_id"":""" + 
                    searchId + 
                    @"""}},""_source"":{""excludes"":[""attachment.content""]}}";

                var content = new StringContent(body, Encoding.UTF8, "application/json");
                response = httpClient.PostAsync(url, content).Result;
            }
            return response;
        }
    }
}
