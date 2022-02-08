using ElasticSearch_Document_API.Services;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Stubs
{
    public class DocumentSaverStub : IDocumentSaver
    {
        public async Task<bool> SaveBase64Document(string doc)
        {
            await Task.Delay(10);
            return true;
        }
    }
}
