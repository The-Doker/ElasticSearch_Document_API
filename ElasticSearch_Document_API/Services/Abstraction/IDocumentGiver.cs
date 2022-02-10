using ElasticSearch_Document_API.Models;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services.Abstraction
{
    public interface IDocumentGiver
    {
        public Task<FileModel> GetDocumentFromSavedFiles(string documentId);
    }
}
