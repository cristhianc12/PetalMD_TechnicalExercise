using Application.Interface.Security;

using Infrastructure.Security.Authorization;
using Infrastructure.Transversal.Core.UserCache;

using Microsoft.Extensions.Caching.Memory;

namespace Application.Service.Security.Authorization
{
    public class AuthService : IAuthService
    {
        private readonly IUsersAppService _usersAppService;
        private IMemoryCache _cache;

        public AuthService(IUsersAppService usersAppService,
            IMemoryCache cache)
        {
            _usersAppService = usersAppService;
            _cache = cache;
        }

        public bool Authorized(string token, string userId, string controller, string action, string RemoteIpAddress,
            string LocalIpAddress, string userAgent)
        {
            bool result = true;

            //Se valida si se limpia la cache cuando cambie el token del usuario
            var oDefaultValuesUserDTO = _cache.Get<DefaultValuesUserDTO>("User_" + userId);
            if (oDefaultValuesUserDTO == null || oDefaultValuesUserDTO.token != token)
            {
                _cache.Remove("User_" + userId);

                oDefaultValuesUserDTO = new DefaultValuesUserDTO();
                oDefaultValuesUserDTO.PathRequest = "/" + controller + "/" + action;
                oDefaultValuesUserDTO.token = token;
            }

            result = true;

            _cache.Set<DefaultValuesUserDTO>("User_" + userId, oDefaultValuesUserDTO);

            return result;
        }
    }
}
