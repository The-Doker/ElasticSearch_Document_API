using ElasticSearch_gRPC_Service.Models;
using System.Collections.Generic;

namespace ElasticSearch_gRPC_Service.Commons
{
    public static class HighlightsParser
    {
        public static List<DocumentSearchResultModel> ParseHighlightsFromDocuments(List<Hit> documents)
        {
            var documentsHighlights = new List<DocumentSearchResultModel>();
            foreach (var document in documents)
            {
                documentsHighlights.Add(new DocumentSearchResultModel()
                {
                    Id = document.Id,
                    Highlights = document.Highlight.AttachmentContent
                });
            }
            return documentsHighlights;
        }
    }
}
