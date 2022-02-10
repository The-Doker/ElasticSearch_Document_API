using ElasticSearch_gRPC_Service.Models;
using System.Collections.Generic;

namespace ElasticSearch_gRPC_Service.Commons
{
    public class FileParser
    {
        public static FileDownloadReply ParseFileFromDocuments(Hit document)
        {
            var fileParsed = new FileDownloadReply()
            {
                DocumentBase64 = document.Source.Data,
                DocumentType = Dictionaries.Dictionaries.fileTypes[document.Source.Attachment.ContentType],
                DocumentName = document.Source.Attachment.Title
            };
            return fileParsed;
        }
    }
}
