using ElasticSearch_Document_API.Services.Abstraction;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Stubs
{
    public class DocumentGiverStub : IDocumentGiver
    {
        public Task<string> GetDocumentFromSavedFiles(string documentId)
        {
            return Task.FromResult("e1xydGYxXGFuc2kNCkxvcmVtIGlwc3VtIGRvbG9yIHNpdCBhbWV0DQpccGFyIH0=");
        }
    }
}
