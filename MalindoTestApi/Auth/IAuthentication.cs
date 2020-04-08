using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MalindoTestAPI.Auth
{
    public interface IAuthentication
    {
        bool IsAuthenticated(HttpRequest request);
    }
}
