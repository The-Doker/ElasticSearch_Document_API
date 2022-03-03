using ElasticSearch_Document_API.Models;
using System.Collections.Generic;

namespace ElasticSearch_Document_API.Data
{
    public static class AccountsDB
    {
        public static List<JwtAuthModel> CorrectAccounts = new List<JwtAuthModel>()
        {
            new JwtAuthModel()
            {
                UserName = "admin@api.com",
                Password = "admin",
                Role = "administrator"
            },
            new JwtAuthModel()
            {
                UserName= "user@api.com",
                Password = "user",
                Role = "apiuser"
            }
        };
    }
}
