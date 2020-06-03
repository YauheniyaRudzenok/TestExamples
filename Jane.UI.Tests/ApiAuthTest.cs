using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	public class ApiAuthTest
	{
		[Test]
		public void Test()
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, "jane"),
				new Claim(ClaimTypes.Surname, "jane"),
			};

			var claimsIdentity = new ClaimsIdentity(
				claims,
				CookieAuthenticationDefaults.AuthenticationScheme);

			var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), 
				CreateAuthProperties("token"), 
				CookieAuthenticationDefaults.AuthenticationScheme);

			var container = ContainerBuilder.Build();
			var dataProtectionProvider = container.GetService<IDataProtectionProvider>();

			var cookieContent = new TicketDataFormat(dataProtectionProvider.CreateProtector("auth")).Protect(ticket);
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
	}
}