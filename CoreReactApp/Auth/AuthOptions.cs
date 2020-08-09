using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoreReactApp.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "CoreReactApp"; // издатель токена
        public const string AUDIENCE = "ClientApp"; // потребитель токена
        public const int LIFETIME = 20; // время жизни токена - 20 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
