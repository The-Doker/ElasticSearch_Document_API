using System.Collections.Generic;

namespace ElasticSearch_gRPC_Service.Dictionaries
{
    public static class Dictionaries
    {
        public static Dictionary<string, string> fileTypes = new Dictionary<string, string>()
        {
            ["application/vnd.openxmlformats-officedocument.wordprocessingml.document"] = "docx",
            ["application/msword"] = "doc",
            ["application/pdf"] = "pdf",
            ["application/rtf"] = "rtf"
        };
    }
}
