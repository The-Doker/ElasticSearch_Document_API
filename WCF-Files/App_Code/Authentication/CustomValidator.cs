using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using WCF_Files.Models;

namespace WCF_Files.App_Code.Authentication
{
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            AccountModel model = new AccountModel();
            if (model.Login(userName, password))
                return;
            throw new SecurityTokenException("Invalid Account");
        }
    }
}