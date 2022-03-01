using ElasticSearch_Document_API.Inspectors;
using ElasticSearch_Document_API.Models;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ElasticSearch_Document_API.Behaviors
{
    public class AuthBehavior : IEndpointBehavior
    {
        private AuthDataModel _authData;

        public AuthBehavior(AuthDataModel authData)
        {
            _authData = authData;
        }
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new AuthInspector(_authData));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

    }
}
