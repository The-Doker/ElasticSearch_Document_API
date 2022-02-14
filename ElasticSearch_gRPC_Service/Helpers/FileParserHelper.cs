using ElasticSearch_gRPC_Service.Models;

namespace ElasticSearch_gRPC_Service.Commons
{
    public class FileParserHelper
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
