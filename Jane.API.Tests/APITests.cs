using System.Collections.Generic;
using Jane.Todo.Dto;
using Jane.UI.Tests.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace Jane.API.Tests
{
	public class APITests
	{
		[Test]
		public void Login()
		{
			//Arrange
			RestClient client = ApiMain.ClientManager.Client;
			//options for post request are: auth, task
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
		//
		[Test]
		public void NewTaskAddedCorrectly()
		{
			//Arrange
			var auth = new Authentication();
			var token = auth.LogIn();

			//Act
			RestClient client = ApiMain.ClientManager.Client;
			client.Authenticator = new JwtAuthenticator(token);
			var postRequest = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			var body = new RequestBuilder().AddCreatedDate()
													.AddDueDate()
													.AddTitle()
													.AddNote()
													.Build();
			postRequest.AddJsonBody(body);

			var responce = client.Post<TodoTaskDto>(postRequest);
			var createdTask = (TodoTaskDto)responce.Data;

			//Assert
			Assert.AreEqual(createdTask.Created, body.Created);
			Assert.AreEqual(createdTask.DueDate, body.DueDate);
			Assert.AreEqual(createdTask.Title, body.Title);
			Assert.AreEqual(createdTask.Note, body.Note);
		}

		[Test]
		public void OnlyOneTaskIsAdded()
		{
			//Arrange
			var auth = new Authentication();
			var token = auth.LogIn();

			//Act
			RestClient client = ApiMain.ClientManager.Client;
			client.Authenticator = new JwtAuthenticator(token);
			var getRequest = new RestRequest("/api/todo", Method.GET);
			var before = client.Get<TodoTaskDto>(getRequest).Content;
			var tasksBefore = JsonConvert.DeserializeObject<List<TodoTaskDto>>(before);

			var postRequest = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			var body = new RequestBuilder().AddCreatedDate()
													.AddDueDate()
													.AddTitle()
													.AddNote()
													.Build();
			postRequest.AddJsonBody(body);

			client.Post<TodoTaskDto>(postRequest);

			var after = client.Get<TodoTaskDto>(getRequest).Content;
			var tasksAfter = JsonConvert.DeserializeObject<List<TodoTaskDto>>(after);

			//Assert
			Assert.AreEqual(tasksAfter.Count, tasksBefore.Count + 1);
		}
		[Test]
		public void DeleteItem()
		{
			//Arrange
			var auth = new Authentication();
			var token = auth.LogIn();

			RestClient client = ApiMain.ClientManager.Client;
			client.Authenticator = new JwtAuthenticator(token);
			var postRequest = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			var body = new RequestBuilder().AddCreatedDate()
													.AddDueDate()
													.AddTitle()
													.AddNote()
													.Build();
			postRequest.AddJsonBody(body);

			var responce = client.Post<TodoTaskDto>(postRequest);
			var createdTask = (TodoTaskDto)responce.Data;
			var path = ("/api/todo/" + createdTask.Id.ToString());

			//Act
			var deleteRequest = new RestRequest(path, Method.DELETE);
			var deleteResponce = client.Delete<TodoTaskDto>(deleteRequest);

			var getRequest = new RestRequest(path, Method.GET);
			var getResponce = client.Get<TodoTaskDto>(getRequest);

			//Assert
			Assert.AreEqual("Internal Server Error", getResponce.StatusDescription);
		}

		[TearDown]
		public void Clean()
		{
			ApiMain.Clean();
		}
	}
}
