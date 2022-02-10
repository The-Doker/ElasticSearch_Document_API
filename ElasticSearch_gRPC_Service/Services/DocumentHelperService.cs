using ElasticSearch_gRPC_Service.Commons;
using Grpc.Core;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ElasticSearch_gRPC_Service
{
    public class DocumentHelperService : DocumentHelper.DocumentHelperBase
    {
        public override Task<UploadFileReply> UploadFileToElastic(UploadFileRequest request, ServerCallContext context)
        {
            HttpRequestSender.SendFileToElastic(request.Base64Data);

            return Task.FromResult(new UploadFileReply
            {
                Result = true
            });
        }

        public override async Task<FileSearchReply> FindFileInElastic(FileSearchRequest request, ServerCallContext context)
        {
            var responseFromElastic = HttpRequestSender.SendSearchRequestToElastic(request.Query);
            var recievedDocuments = await ElasticReplyParser.ParseElasticReplyToJson(responseFromElastic);
            var documentsHighlights = HighlightsParser.ParseHighlightsFromDocuments(recievedDocuments);

            return new FileSearchReply
            {
                JsonData = JsonConvert.SerializeObject(documentsHighlights)
            };
        }

        public override async Task<FileDownloadReply> DownloadFileFromElastic(FileDownloadRequest request, ServerCallContext context)
        {
            var responseFromElastic = HttpRequestSender.SendDownloadRequestToElastic(request.DocumentId);
            var recievedDocuments = await ElasticReplyParser.ParseElasticReplyToJson(responseFromElastic);
            var fileFromElastic = FileParser.ParseFileFromDocuments(recievedDocuments[0]);
            return fileFromElastic;
        }
    }
}
