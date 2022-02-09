using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ElasticSearch_gRPC_Service.Models
{
    public class Attachment
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "content_type")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "content_length")]
        public int ContentLength { get; set; }
    }

    public class Source
    {
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "attachment")]
        public Attachment Attachment { get; set; }
    }

    public class Highlight
    {
        [JsonProperty(PropertyName = "attachment.content")]
        public List<string> AttachmentContent { get; set; }
    }

    public class Hit
    {
        [JsonProperty(PropertyName = "_index")]
        public string Index { get; set; }

        [JsonProperty(PropertyName = "_type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_score")]
        public double Score { get; set; }

        [JsonProperty(PropertyName = "_ignored")]
        public List<string> Ignored { get; set; }

        [JsonProperty(PropertyName = "_source")]
        public Source Source { get; set; }

        [JsonProperty(PropertyName = "highlight")]
        public Highlight Highlight { get; set; }
    }

    public class SearchResponse
    {
        [JsonProperty(PropertyName = "hits")]
        public List<Hit> Hits { get; set; }
    }


}
