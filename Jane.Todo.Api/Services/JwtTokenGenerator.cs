using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Jane.Todo.Api.Services
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		public string GenerateAccessToken(string userName, IEnumerable<Claim> userClaims)
		{
			var expiration = TimeSpan.FromDays(TokenOptions.TokenExpiryInDays);
			var jwt = new JwtSecurityToken(issuer: TokenOptions.Issuer,
										   audience: TokenOptions.Audience,
										   claims: MergeUserClaimsWithDefaultClaims(userName, userClaims),
										   notBefore: DateTime.UtcNow,
										   expires: DateTime.UtcNow.Add(expiration),
										   signingCredentials: new SigningCredentials(TokenOptions.SecurityKey, SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		private static IEnumerable<Claim> MergeUserClaimsWithDefaultClaims(string userName, IEnumerable<Claim> userClaims)
		{
			var claims = new List<Claim>(userClaims)
			{
				new Claim(ClaimTypes.Name, userName),
				new Claim(JwtRegisteredClaimNames.Sub, userName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(), ClaimValueTypes.Integer64)
			};

			return claims;
		}
	}
}