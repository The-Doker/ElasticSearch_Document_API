using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using WCF_Files.Contexts;
using WCF_Files.Headers;
using WCF_Files.Models;

namespace WCF_Files.Inspectors
{
    public class AuthMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Create a copy of the original message so that we can mess with it.
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();
            Message messageCopy = buffer.CreateMessage();

            // Read the custom context data from the headers
            AuthDataModel customData = SoapAuthHeader.ReadHeader(request);

            // Add an extension to the current operation context so
            // that our custom context can be easily accessed anywhere.
            ServerAuthContext customContext = new ServerAuthContext();

            if (customData != null)
            {
                customContext.userName = customData.userName;
                customContext.password = customData.password;
            }
            OperationContext.Current.IncomingMessageProperties.Add(
                     "CurrentContext", customContext);
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}