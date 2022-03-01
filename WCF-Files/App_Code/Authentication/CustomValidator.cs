using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WCF_Files.Contexts;
using WCF_Files.Models;

namespace WCF_Files.App_Code.Authentication
{
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            //var authDataFormContext = (ServerAuthContext)OperationContext.Current.IncomingMessageProperties["CurrentContext"];
            AccountModel model = new AccountModel();
            /*if (model.Login(authDataFormContext.userName, authDataFormContext.password))
                return;
            throw new SecurityTokenException("Invalid Account from Inspector");*/
            if (model.Login(userName, password))
                return;
            throw new SecurityTokenException("Invalid Account");
        }
    }
}