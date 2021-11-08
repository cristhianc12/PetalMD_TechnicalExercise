using Application.Adapter.Core;
using Application.DTO.Security;
using Application.Interface.Security;
using Domain.Security.Repositories;

using Infrastructure.Security.JWT;

using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Service.Security
{
    public class UsersAppService : IUsersAppService
    {
		private readonly IJwtFactory _jwtFactory;
		private readonly IConfiguration _configuration;
		//private readonly IUsersRepository _usersRepository;
		private readonly IAdapter _adapter;
        private readonly IConfiguration _config;

        public UsersAppService(/*IUsersRepository usersRepository,*/
			IJwtFactory jwtFactory,
			IConfiguration configuration,
            IAdapter adapter,
            IConfiguration config)
        {
            //_usersRepository = usersRepository;
			_jwtFactory = jwtFactory;
			_configuration = configuration;
            _adapter = adapter;
            _config = config;
        }

        public async Task<string> Login(CredentialDTO oCredentialDTO)
        {
            var identity = await GetClaimsIdentity(oCredentialDTO);
            var expirationToken = 24;

            return (identity != null) ? _jwtFactory.GenerateEncodedToken(oCredentialDTO.UserId, identity, _configuration["APIKeyJWT"], expirationToken) : null;
        }

		private async Task<ClaimsIdentity> GetClaimsIdentity(CredentialDTO oCredentialDTO)
		{
			if (string.IsNullOrEmpty(oCredentialDTO.UserId) || string.IsNullOrEmpty(oCredentialDTO.Password))
				return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = new UsersDTO
            {
                UserId = oCredentialDTO.UserId
            };

            return await this.CreateAccesUserAndClaims(userToVerify, oCredentialDTO);
        }

        /// <summary>
        /// Token generation
        /// </summary>
        /// <param name="userToVerify"></param>
        /// <param name="oCredentialDTO"></param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> CreateAccesUserAndClaims(UsersDTO userToVerify, CredentialDTO oCredentialDTO)
        {

            return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userToVerify, Guid.NewGuid().ToString()));
        }

    }
}
