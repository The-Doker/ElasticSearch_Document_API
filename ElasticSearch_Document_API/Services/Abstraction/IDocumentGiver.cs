using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Abstraction
{
    public interface IDocumentGiver
    {
        public Task<string> GetDocumentFromSavedFiles(string documentId);
    }
}
