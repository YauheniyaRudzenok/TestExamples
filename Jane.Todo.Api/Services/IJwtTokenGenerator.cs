using System.Collections.Generic;
using System.Security.Claims;

namespace Jane.Todo.Api.Services
{
	public interface IJwtTokenGenerator
	{
		string GenerateAccessToken(string userName, IEnumerable<Claim> userClaims);
	}
}