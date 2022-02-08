using ElasticSearch_Document_API.Services;

namespace ElasticSearch_Document_API.Stubs
{
    public class DocumentSaverStub : IDocumentSaver
    {
        public bool SaveBase64Document(string doc)
        {
            return true;
        }
    }
}
