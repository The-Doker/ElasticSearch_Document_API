using System.Runtime.Serialization;

namespace ElasticSearch_Document_API.Models
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
