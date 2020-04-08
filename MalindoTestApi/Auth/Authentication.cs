using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MalindoTestAPI.Auth
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _config;

        public Authentication(IConfiguration config)
        {            
            _config = config;
        }
        public bool IsAuthenticated(HttpRequest request)
        {
            var authKeyProvided = request.Headers.ContainsKey("Authorization");
            var authKey = authKeyProvided ? request.Headers.ToList().FirstOrDefault(k => k.Key == "Authorization").Value[0] : string.Empty;
            var apiKey = _config["Secrets:ApiKey"];


            // instead of a hardcoded value, this key should be coming from a secret file which is bound at application startup. 
            // and those keys can be injected at deployment time from a secure location like Azure Key Vault instead of keeping them within the solution
            if (string.IsNullOrEmpty(authKey) || authKey != apiKey)
            {
                return false;
            }

            return true;
        }
    }
}
