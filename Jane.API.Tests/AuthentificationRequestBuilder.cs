using System;
using System.Collections.Generic;
using System.Text;
using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;
using Microsoft.Extensions.Configuration;

namespace Jane.API.Tests
{
	public class AuthentificationRequestBuilder
	{
		private readonly AuthenticationRequestDto request;
		private IConfigurationRoot configuration;

		public AuthentificationRequestBuilder()
		{
			configuration = Config.Instance;
			request = new AuthenticationRequestDto();
		}

		public AuthentificationRequestBuilder AddName()
		{
			request.UserName = configuration["appCredentials:name"];
			return this;
		}

		public AuthentificationRequestBuilder AddPassword()
		{
			request.Password = configuration["appCredentials:password"];
			return this;
		}

		public AuthenticationRequestDto Build() => request;
	}
}
