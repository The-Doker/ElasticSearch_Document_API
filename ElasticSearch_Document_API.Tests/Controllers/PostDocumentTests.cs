using ElasticSearch_Document_API.Controllers;
using ElasticSearch_Document_API.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Tests
{
    public class PostDocumentTests
    {
        private Mock<IDocumentSaver> _mockDocumentSaver;
        private PostDocumentController _controller;
        private IFormFile _fakeFile;
        private MemoryStream _stream;

        [SetUp]
        public void Setup()
        {
            _mockDocumentSaver = new Mock<IDocumentSaver>();
            _controller = new PostDocumentController(_mockDocumentSaver.Object);
            _stream = CreateStreamForFakeFile("Hello World from a Fake File");
            

        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task TakeFileFromFormData(bool shouldBeCorrectFile)
        {
            //Arrange
            if (shouldBeCorrectFile)
                _fakeFile = new FormFile(_stream, 0, _stream.Length, "id_from_form", "fakeFile.pdf");
            else _fakeFile = null;

            _mockDocumentSaver.Setup(_ => _.SaveBase64Document(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            //Act
            var result = await _controller.Post(_fakeFile);

            //Assert
            Assert.AreEqual(shouldBeCorrectFile, result);
        }

        private MemoryStream CreateStreamForFakeFile(string content)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}