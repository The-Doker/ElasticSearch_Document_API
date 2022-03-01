using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCF_Files.Models
{
    [DataContract]
    public class AuthDataModel
    {
        public AuthDataModel()
        {

        }
        public AuthDataModel(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public string password { get; set; }
    }
}