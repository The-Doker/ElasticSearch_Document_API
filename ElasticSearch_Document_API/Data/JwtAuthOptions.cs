using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ElasticSearch_Document_API.Data
{
    public class JwtAuthOptions
    {
        public const string ISSUER = "ElasticApi"; 
        public const string AUDIENCE = "ElasticApiClient"; 
        const string KEY = "keyForElasticApi";   
        public const int LIFETIME = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
