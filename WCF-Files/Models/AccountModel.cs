using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF_Files.Models
{
    public class AccountModel
    {
        public List<Account> Accounts = new List<Account>();
        public AccountModel()
        {
            Accounts.Add(new Account() { UserName = "wcf", Password = "wcf"});
        }
        public bool Login(string login, string password)
        {
            return Accounts.Any(_ => _.UserName == login && _.Password == password);
        }
    }
}