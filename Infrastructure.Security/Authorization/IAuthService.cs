using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Security.Authorization
{
    public interface IAuthService
    {
        bool Authorized(string token, string userId, string controller, string action, string RemoteIpAddress, 
            string LocalIpAddress, string userAgent);
    }
}
