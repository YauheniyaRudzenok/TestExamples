using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Jane.UI.Tests
{
	public static class ContainerBuilder
	{
		public static IServiceProvider Build()
		{
			ServiceCollection services = new ServiceCollection();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();

			services.AddDataProtection()
				.SetApplicationName("todo-jane");

			return services.BuildServiceProvider();
		}
	}
}