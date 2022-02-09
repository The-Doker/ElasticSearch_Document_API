using System.Collections.Generic;

namespace ElasticSearch_gRPC_Service.Models
{
    public class DocumentSearchResultModel
    {
        public string Id { get; set; }
        public List<string> Highlights { get; set; }
    }
}
