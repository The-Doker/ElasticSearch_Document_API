using ElasticSearch_gRPC_Service.Commons;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch_gRPC_Service.Tests
{
    public class ParserTests
    {
        private HttpResponseMessage _fakeResponse;
        private string _jsonResponseFromElastic;
        private string _expectedBodyOfResponse;
        private string _expectedTitleOfDocument;
        private string _expectedIdOfDocument;

        [TestCase(HttpStatusCode.OK)]
        public async Task ElasticReplyParserTests(HttpStatusCode currentCode)
        {
            //Arrange
            _fakeResponse = new HttpResponseMessage(currentCode)
            {
                Content = new ByteArrayContent(Encoding.UTF8.GetBytes(_jsonResponseFromElastic))
            };

            //Act
            var result = await ElasticReplyParserHelper.ParseElasticReplyToJson(_fakeResponse);

            //Assert
            Assert.AreEqual(_expectedBodyOfResponse, result[0].Highlight.AttachmentContent[0]);
        }

        [TestCase(HttpStatusCode.OK)]
        public async Task ParseFileFromDocumentsTests(HttpStatusCode currentCode)
        {
            //Arrange
            _fakeResponse = new HttpResponseMessage(currentCode)
            {
                Content = new ByteArrayContent(Encoding.UTF8.GetBytes(_jsonResponseFromElastic))
            };
            var parsedResponseFromElastic = await ElasticReplyParserHelper.ParseElasticReplyToJson(_fakeResponse);

            //Act
            var result = FileParserHelper.ParseFileFromDocuments(parsedResponseFromElastic[0]);

            //Assert
            Assert.AreEqual(_expectedTitleOfDocument, result.DocumentName);
        }

        [TestCase(HttpStatusCode.OK)]
        public async Task ParseHighlightsFromDocumentsTest(HttpStatusCode currentCode)
        {
            //Arrange
            _fakeResponse = new HttpResponseMessage(currentCode)
            {
                Content = new ByteArrayContent(Encoding.UTF8.GetBytes(_jsonResponseFromElastic))
            };
            var parsedResponseFromElastic = await ElasticReplyParserHelper.ParseElasticReplyToJson(_fakeResponse);

            //Act
            var result = HighlightsParserHelper.ParseHighlightsFromDocuments(parsedResponseFromElastic);

            //Assert
            Assert.AreEqual(_expectedIdOfDocument, result[0].Id);
        }

        [SetUp]
        public void Setup()
        {
            _expectedBodyOfResponse = "This is a fake document from ElasticSearch API";
            _expectedTitleOfDocument = "Elastic";
            _expectedIdOfDocument = "-5ya3X4Bf3wEO9d-MzlZ";

            _jsonResponseFromElastic = @"{
  ""took"" : 61,
  ""timed_out"" : false,
  ""_shards"" : {
    ""total"" : 1,
    ""successful"" : 1,
    ""skipped"" : 0,
    ""failed"" : 0
  },
  ""hits"" : {
    ""total"" : {
      ""value"" : 1,
      ""relation"" : ""eq""
    },
    ""max_score"" : 1.335186,
    ""hits"" : [
      {
        ""_index"" : ""documenthelper"",
        ""_type"" : ""_doc"",
        ""_id"" : ""-5ya3X4Bf3wEO9d-MzlZ"",
        ""_score"" : 1.335186,
        ""_ignored"" : [
          ""attachment.content.keyword"",
          ""data.keyword""
        ],
        ""_source"" : {
		  ""data"" : ""e1xydGYxXGFuc2kNCkxvcmVtIGlwc3VtIGRvbG9yIHNpdCBhbWV0DQpccGFyIH0="",
          ""attachment"" : {
            ""date"" : ""2020-03-19T05:43:00Z"",
            ""content_type"" : ""application/rtf"",
            ""author"" : ""Александр"",
            ""language"" : ""ru"",
            ""title"" : ""Elastic"",
            ""content_length"" : 3342
          }
        },
        ""highlight"" : {
          ""attachment.content"" : [
            ""This is a fake document from ElasticSearch API""
          ]
        }
      }
    ]
  }
}
";
        }
    }
}