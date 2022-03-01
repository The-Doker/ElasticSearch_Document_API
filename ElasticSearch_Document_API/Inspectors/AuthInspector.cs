using ElasticSearch_Document_API.Models;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace ElasticSearch_Document_API.Inspectors
{
    public class AuthInspector : IClientMessageInspector
    {
        private AuthDataModel _authData;
        public AuthInspector(AuthDataModel authData)
        {
            _authData = authData;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();

            SoapAuthHeader header = new SoapAuthHeader(_authData);

            request.Headers.Add(header);

            return null;
        }
    }
}
