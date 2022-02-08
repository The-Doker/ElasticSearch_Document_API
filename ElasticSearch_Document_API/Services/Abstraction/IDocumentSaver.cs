using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Services
{
    public interface IDocumentSaver
    {
        public Task<bool> SaveBase64Document(string doc);
    }
}
