namespace ElasticSearch_Document_API.Services
{
    public interface IDocumentSaver
    {
        public bool SaveBase64Document(string doc);
    }
}
