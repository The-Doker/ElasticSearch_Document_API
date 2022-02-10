using ElasticSearch_Document_API.Models;
using ElasticSearch_Document_API.Services.Abstraction;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Stubs
{
    public class DocumentGiverStub : IDocumentGiver
    {
        public Task<FileModel> GetDocumentFromSavedFiles(string documentId)
        {
            var result = new FileModel() { DataBase64 = "e1xydGYxXGFuc2kNCkxvcmVtIGlwc3VtIGRvbG9yIHNpdCBhbWV0DQpccGFyIH0=", Name = "file", Type = "rtf" };
            return Task.FromResult(result);
        }
    }
}
