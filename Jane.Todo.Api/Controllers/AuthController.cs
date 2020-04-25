using System.Collections.Generic;
using System.Security.Claims;
using Jane.Todo.Api.Dto;
using Jane.Todo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jane.Todo.Api.Controllers
{
	[Route("api/auth")]
	public class AuthController : Controller
	{
		private readonly ISecurityService securityService;
		private readonly IJwtTokenGenerator jwtTokenGenerator;

		public AuthController(ISecurityService securityService)
		{
			this.securityService = securityService;
		}

		[HttpPost]
		public IActionResult Post([FromBody] AuthorizationRequestDto dto)
		{
			var success = this.securityService.SignIn(dto.UserName, dto.Password);

			if (!success) return this.Unauthorized();

			var accessToken = this.jwtTokenGenerator.GenerateAccessToken(dto.UserName, ToClaim(dto));

			return this.Json(new AuthorizationResultDto()
			{
				Token = accessToken,
				UserName = dto.UserName,
			});
		}

		private IEnumerable<Claim> ToClaim(AuthorizationRequestDto dto) =>
			new List<Claim>
			{
				new Claim(ClaimTypes.GivenName, dto.UserName),
				new Claim(ClaimTypes.Surname, dto.UserName)
			};
	}
}