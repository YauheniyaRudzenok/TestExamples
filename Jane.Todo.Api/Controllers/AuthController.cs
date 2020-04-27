using System.Collections.Generic;
using System.Security.Claims;
using Jane.Todo.Api.Services;
using Jane.Todo.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Jane.Todo.Api.Controllers
{
	[Route("api/auth")]
	public class AuthController : Controller
	{
		private readonly ISecurityService securityService;
		private readonly IJwtTokenGenerator jwtTokenGenerator;

		public AuthController(ISecurityService securityService, IJwtTokenGenerator jwtTokenGenerator)
		{
			this.securityService = securityService;
			this.jwtTokenGenerator = jwtTokenGenerator;
		}

		[HttpPost]
		public IActionResult Post([FromBody] AuthenticationRequestDto dto)
		{
			var success = this.securityService.SignIn(dto.UserName, dto.Password);

			if (!success) return this.Unauthorized();

			var accessToken = this.jwtTokenGenerator.GenerateAccessToken(dto.UserName, ToClaim(dto));

			return this.Json(new AuthenticationResultDto()
			{
				Token = accessToken,
				UserName = dto.UserName,
			});
		}

		private IEnumerable<Claim> ToClaim(AuthenticationRequestDto dto) =>
			new List<Claim>
			{
				new Claim(ClaimTypes.GivenName, dto.UserName),
				new Claim(ClaimTypes.Surname, dto.UserName)
			};
	}
}