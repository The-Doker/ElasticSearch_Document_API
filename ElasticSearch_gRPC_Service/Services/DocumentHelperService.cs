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
        public override async Task<UploadFileReply> UploadFileToElastic(UploadFileRequest request, ServerCallContext context)
        {
            if (request.Base64Data != null)
                _logger.LogInformation("gRPC ������� ������� ���� ��� �������� � ElasticSearch");
            var responseFromElastic = await _elasticClient.SendFileToElasticAsync(request.Base64Data);
            if (responseFromElastic.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _logger.LogInformation("gRPC ������� �������� ���� � ElasticSearch");
                return new UploadFileReply
                {
                    Result = true
                };
            }
            _logger.LogError("gRPC �� ���� �������� ���� � ElasticSearch");
            return new UploadFileReply
            {
                Result = false
            };

        }

        public override async Task<FileSearchReply> FindFileInElastic(FileSearchRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC ������� ������� ��������� ������");
            var responseFromElastic = await _elasticClient.SendSearchRequestToElasticAsync(request.Query);
            _logger.LogInformation("gRPC ������� ������� ����� �� ElasticSearch");
            var recievedDocuments = await ElasticReplyParserHelper.ParseElasticReplyToJson(responseFromElastic);
            var documentsHighlights = HighlightsParserHelper.ParseHighlightsFromDocuments(recievedDocuments);
            _logger.LogInformation("gRPC ������� �������� ����� �� ElasticSearch");

            return new FileSearchReply
            {
                JsonData = JsonConvert.SerializeObject(documentsHighlights)
            };
        }

        public override async Task<FileDownloadReply> DownloadFileFromElastic(FileDownloadRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC ������� ������� ������ �� �������� �����");
            var responseFromElastic = await _elasticClient.SendDownloadRequestToElasticAsync(request.DocumentId);
            _logger.LogInformation("gRPC ������� ������� ����� �� ElasticSearch");
            var recievedDocuments = await ElasticReplyParserHelper.ParseElasticReplyToJson(responseFromElastic);
            var fileFromElastic = FileParserHelper.ParseFileFromDocuments(recievedDocuments[0]);
            _logger.LogInformation("gRPC ������� �������� ����� �� ElasticSearch");
            return fileFromElastic;
        }
    }
}
