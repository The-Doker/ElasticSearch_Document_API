using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace WCF_Files.Behavior.Extensions
{
    public class AuthBehaviorExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new AuthBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(AuthBehavior);
            }
        }
    }
}