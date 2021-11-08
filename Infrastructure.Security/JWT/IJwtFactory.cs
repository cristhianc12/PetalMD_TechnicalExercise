using Application.DTO.Security;

using System.Security.Claims;

namespace Infrastructure.Security.JWT
{
    public interface IJwtFactory
	{
		string GenerateEncodedToken(string userName, ClaimsIdentity identity, string APIKeyJWT, int expirationToken);
		/// <summary>
		/// Genrea los claims de jwt
		/// </summary>
		/// <param name="user"></param>
		/// <param name="jti"></param>
		/// <returns></returns>
		ClaimsIdentity GenerateClaimsIdentity(UsersDTO user, string jti);
	}
}
