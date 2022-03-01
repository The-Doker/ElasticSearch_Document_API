using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WCF_Files.Contexts
{
    public class ServerAuthContext : IExtension<OperationContext>
    {
        public string userName;
        public string password;
        
        public static ServerAuthContext Current
        {
            get
            {
                return OperationContext.Current.Extensions.Find<ServerAuthContext>();
            }
        }
        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
    }
}