using ElasticSearch_gRPC_Service.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElasticSearch_gRPC_Service.Commons
{
    public static class ElasticReplyParserHelper
    {
        public static async Task<List<Hit>> ParseElasticReplyToJson(HttpResponseMessage elasticReply)
        {
            var responseDeserialized = JToken.Parse(await elasticReply.Content.ReadAsStringAsync())
                .SelectTokens("hits")
                .Select(x => x.ToObject<SearchResponse>())
                .ToList();
            return responseDeserialized[0].Hits;
        }
    }
}
