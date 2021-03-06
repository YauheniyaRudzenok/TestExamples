﻿using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;
using RestSharp;

namespace Jane.UI.Tests.Infrastructure
{
	public class Authentication
	{
		public string LogIn()
		{

			var configuration = Config.Instance;

			var client = new RestClient(configuration["appSettings:apiURL"]);
			var request = new RestRequest("/api/auth", Method.POST);
			request.AddJsonBody(new AuthenticationRequestDto
			{
				UserName = configuration["appCredentials:name"],
				Password = configuration["appCredentials:password"]
			});

			var response = client.Post<AuthenticationResultDto>(request);
			string token = response.Data.Token;

			return token;
		}
	}
}
