using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;
using NUnit.Framework;
using RestSharp;

namespace Jane.API.Tests
{
	public class Auth
	{
		[Test]
		public void Login()
		{
			//Arrange
			var client = new RestClient(Config.Instance["appSettings:apiURL"]);
			var request = new RestRequest("/api/auth", Method.POST);
			request.AddJsonBody(new AuthentificationRequestBuilder().AddName()
																	.AddPassword()
																	.Build());

			//Act
			var response = client.Post<AuthenticationResultDto>(request);

			//Assert
			Assert.IsTrue(response.StatusCode == (System.Net.HttpStatusCode)200);
			Assert.That(response.Data.Token, Is.Not.Null);
		}
	}
}