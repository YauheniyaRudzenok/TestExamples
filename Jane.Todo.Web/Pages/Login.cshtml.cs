using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Jane.Todo.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jane.Todo.Web.Pages
{
	[AllowAnonymous]
	public class LoginModel : PageModel
    {
		private readonly IAuthService authService;

		public LoginModel(IAuthService authService)
		{
			this.authService = authService;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			//if(this.Input == null)
			//{
			//	this.Input.UserName = "jane";
			//	this.Input.Password = "password";
			//}

			if (!ModelState.IsValid)
			{
				return Page();
			}

			var authResult = await this.authService.SignIn(this.Input.UserName, this.Input.Password);

			if (string.IsNullOrWhiteSpace(authResult?.Token))
			{
				return this.LocalRedirect("/loginfailed");
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, authResult.UserName),
				new Claim(ClaimTypes.Surname, authResult.UserName),
			};

			var claimsIdentity = new ClaimsIdentity(
				claims,
				CookieAuthenticationDefaults.AuthenticationScheme);

			await this.HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				CreateAuthProperties(authResult.Token));

			return LocalRedirect(Url.Content("~/"));
		}

		private static AuthenticationProperties CreateAuthProperties(string accessToken)
		{
			var authProps = new AuthenticationProperties();
			authProps.StoreTokens(
				new[]
				{
					new AuthenticationToken()
					{
						Name = "jwt",
						Value = accessToken
					}
				});

			return authProps;
		}

		public class InputModel
		{
			[Required]
			[MaxLength(100)]
			public string UserName { get; set; }

			[DataType(DataType.Password)]
			[Required]
			[MaxLength(20)]
			public string Password { get; set; }
		}
	}
}