using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using TypeServiceClient = GetDataService.GetDataServiceClient;

namespace ElasticSearch_Document_API.Facroties
{
    public class WcfClientFactory
    {
        public static GetDataService.IGetDataService CreateChannel(IEndpointBehavior behavior, string url, string username, string password, bool ignoreSsl)
        {
            var binding = new BasicHttpBinding()
            {
                Name = "WcfTypesService"
            };

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
            var endpoint = new EndpointAddress(url);
            var client = new TypeServiceClient(binding, endpoint);
            if (ignoreSsl)
            {
               client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
               new X509ServiceCertificateAuthentication()
               {
                   CertificateValidationMode = X509CertificateValidationMode.None,
                   RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck
               };
            }
            client.Endpoint.EndpointBehaviors.Add(behavior);
            client.ClientCredentials.UserName.UserName = username;
            client.ClientCredentials.UserName.Password = password;
            return client;
        }
    }
}
