using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RestSharp;

namespace Jane.API.Tests
{
	public class Auth
	{
		IConfigurationRoot configuration;

		[OneTimeSetUp]
		public void BuildConfig()
		{
			configuration = new Config().BuildConfig();
		}

		[Test]
		public void Login()
		{
			//Arrange
			var client = new RestClient(configuration["appSettings:apiURL"]);
			var request = new RestRequest("/api/auth", Method.POST);
			request.AddJsonBody(new AuthenticationRequestDto
			{
				UserName = configuration["appCredentials:name"],
				Password = configuration["appCredentials:password"]
			});

			//Act
			var response = client.Post<AuthenticationResultDto>(request);

			//Assert
			Assert.IsTrue(response.StatusCode == (System.Net.HttpStatusCode)200);
			Assert.That(response.Data.Token, Is.Not.Null);
		}
	}
}