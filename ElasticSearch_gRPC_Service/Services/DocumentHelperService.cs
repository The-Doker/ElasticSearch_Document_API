using ElasticSearch_gRPC_Service.Commons;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ElasticSearch_gRPC_Service
{
    public class DocumentHelperService : DocumentHelper.DocumentHelperBase
    {
        private IElasticClient _elasticClient;
        private ILogger<DocumentHelperService> _logger;
        public DocumentHelperService(IElasticClient elasticClient, ILogger<DocumentHelperService> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }
        public override Task<UploadFileReply> UploadFileToElastic(UploadFileRequest request, ServerCallContext context)
        {
            if (request.Base64Data != null)
                _logger.LogInformation("gRPC успешно получил файл для отправки в ElasticSearch");
            _elasticClient.SendFileToElastic(request.Base64Data);
            _logger.LogInformation("gRPC успешно отправил файл в ElasticSearch");
            return Task.FromResult(new UploadFileReply
            {
                Result = true
            });
        }

        public override async Task<FileSearchReply> FindFileInElastic(FileSearchRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC успешно получил поисковой запрос");
            var responseFromElastic = _elasticClient.SendSearchRequestToElastic(request.Query);
            _logger.LogInformation("gRPC успешно получил ответ от ElasticSearch");
            var recievedDocuments = await ElasticReplyParserHelper.ParseElasticReplyToJson(responseFromElastic);
            var documentsHighlights = HighlightsParserHelper.ParseHighlightsFromDocuments(recievedDocuments);
            _logger.LogInformation("gRPC успешно разобрал ответ от ElasticSearch");

            return new FileSearchReply
            {
                JsonData = JsonConvert.SerializeObject(documentsHighlights)
            };
        }

        public override async Task<FileDownloadReply> DownloadFileFromElastic(FileDownloadRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC успешно получил запрос на загрузку файла");
            var responseFromElastic = _elasticClient.SendDownloadRequestToElastic(request.DocumentId);
            _logger.LogInformation("gRPC успешно получил ответ от ElasticSearch");
            var recievedDocuments = await ElasticReplyParserHelper.ParseElasticReplyToJson(responseFromElastic);
            var fileFromElastic = FileParserHelper.ParseFileFromDocuments(recievedDocuments[0]);
            _logger.LogInformation("gRPC успешно разобрал ответ от ElasticSearch");
            return fileFromElastic;
        }
    }
}
