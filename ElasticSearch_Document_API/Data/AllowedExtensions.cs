using System.Collections.Generic;

namespace ElasticSearch_Document_API.Data
{
    public static class AllowedExtensions
    {
        public static List<string> AllowedExtensionsList = new List<string>()
        {
            "doc",
            "docx",
            "pdf",
            "rtf"
        };
    }
}
