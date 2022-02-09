using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Abstraction
{
    public interface IDocumentSearcher
    {
        public Task<string> FindInSavedDocuments(string searchQuery);
    }
}
