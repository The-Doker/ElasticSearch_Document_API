using ElasticSearch_Document_API.Services.Abstraction;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Stubs
{
    public class DocumentSearcherStub : IDocumentSearcher
    {
        public Task<string> FindInSavedDocuments(string searchQuery)
        {
            return Task.FromResult("It's a stub");
        }
    }
}
