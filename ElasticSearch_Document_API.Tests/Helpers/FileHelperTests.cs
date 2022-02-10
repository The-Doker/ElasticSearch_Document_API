using ElasticSearch_Document_API.Controllers;
using ElasticSearch_Document_API.Helpers;
using ElasticSearch_Document_API.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Tests
{
    public class FileHelperTests
    {
        private string _fileInBase64;
        private string _fileName;

        [SetUp]
        public void Setup()
        {
            _fileInBase64 = "e1xydGYxXGFuc2kNCkxvcmVtIGlwc3VtIGRvbG9yIHNpdCBhbWV0DQpccGFyIH0=";
            _fileName = "document.rtf";
        }

        [Test]
        public async Task MakeHttpResponceFromBase64Tests()
        {
            //Act
            var httpResponse = FileHelper.MakeHttpResponceFromBase64(_fileInBase64, _fileName);
            using (var fs = new FileStream(_fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await httpResponse.Content.CopyToAsync(fs);
            }
            var result = File.ReadAllText(_fileName);

            //Assert
            Assert.IsTrue(result.Contains("Lorem ipsum dolor sit amet"));
        }
    }
}