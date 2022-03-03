using ElasticSearch_Document_API.Controllers;
using ElasticSearch_Document_API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Tests
{
    public class SearchDocumentTests
    {
        private SearchDocumentController _controller;
        private Mock<IDocumentSearcher> _documentSearcher;

        [SetUp]
        public void Setup()
        {
            _documentSearcher = new Mock<IDocumentSearcher>();
            _controller = new SearchDocumentController(_documentSearcher.Object);
        }

        [TestCase("It's a stub!")]
        public async Task ReturningJson(string expectedString)
        {
            //Arrange
            _documentSearcher.Setup(_ => _.FindInSavedDocuments(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedString));

            //Act
            var result = (JsonResult) await _controller.Post("searchQuery");
            

            //Assert
            Assert.AreEqual(expectedString, result.Value);
        }
    }
}