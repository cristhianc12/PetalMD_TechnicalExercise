using Application.DTO.Security;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Infrastructure.Security.JWT
{
    public class JwtFactory : IJwtFactory
    {
		private readonly JwtOptions _jwtOptions;

		public JwtFactory(IOptions<JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;

			if (_jwtOptions == null) throw new ArgumentNullException(nameof(jwtOptions));
			if (_jwtOptions.ValidFor <= TimeSpan.Zero)
			{
				throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtOptions.ValidFor));
			}
			if (_jwtOptions.SigningCredentials == null)
			{
				throw new ArgumentNullException(nameof(JwtOptions.SigningCredentials));
			}
			if (_jwtOptions.JtiGenerator == null)
			{
				throw new ArgumentNullException(nameof(JwtOptions.JtiGenerator));
			}
		}

		public string GenerateEncodedToken(string userName, ClaimsIdentity identity, string APIKeyJWT, int expirationToken)
		{
			try
			{
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APIKeyJWT));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var expiration = DateTime.Now.AddHours(expirationToken);

				JwtSecurityToken token = new JwtSecurityToken(
						issuer: _jwtOptions.Issuer,
						audience: _jwtOptions.Audience,
						claims: identity.Claims,
						notBefore: _jwtOptions.NotBefore,
						expires: expiration,
						signingCredentials: creds
						);
				var result =
				 JsonConvert.SerializeObject(new
				 {
					 token = new JwtSecurityTokenHandler().WriteToken(token),
					 expiration,
					 UserName = userName
				 });
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// Genrea los claims de jwt
		/// </summary>
		/// <param name="user"></param>
		/// <param name="jti"></param>
		/// <param name="cultureName"></param>
		/// <param name="idAplicacion"></param>
		/// <returns></returns>
		public ClaimsIdentity GenerateClaimsIdentity(UsersDTO user, string jti)
		{
			return new ClaimsIdentity(new GenericIdentity(user.UserId, "Token"), new[]
			{
				new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId),
				new Claim(JwtRegisteredClaimNames.Jti, jti)
			});
		}
	}
}
