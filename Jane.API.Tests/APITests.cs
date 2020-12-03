using System.Collections.Generic;
using Jane.Todo.Dto;
using Jane.UI.Tests.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace Jane.API.Tests
{
    public class APITests
	{
		readonly Logger log = LogManager.GetCurrentClassLogger();

		[Test]
		public void Login()
		{
			//Arrange
			Log.TestTitle(log, TestContext.CurrentContext.Test.Name);
			RestClient client = ApiMain.ClientManager.Client;
			Log.ClientCreation(log);
			//options for post request are: auth, task
			var request = new RestRequest("/api/auth", Method.POST);
			request.AddJsonBody(new AuthentificationRequestBuilder().AddName()
																	.AddPassword()
																	.Build());

			//Act
			var response = client.Post<AuthenticationResultDto>(request);
			Log.StatusCode(log, response, "200,OK");

			//Assert
			Assert.IsTrue(response.StatusCode == (System.Net.HttpStatusCode)200);
			Assert.That(response.Data.Token, Is.Not.Null);
		}
		//
		[Test]
		public void NewTaskAddedCorrectly()
		{
			//Arrange
			Log.TestTitle(log, TestContext.CurrentContext.Test.Name);
			var auth = new Authentication();
			var token = auth.LogIn();

			//Act
			RestClient client = ApiMain.ClientManager.Client;
			Log.ClientCreation(log);
			client.Authenticator = new JwtAuthenticator(token);
			var postRequest = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			var body = new RequestBuilder().AddCreatedDate()
													.AddDueDate()
													.AddTitle()
													.AddNote()
													.Build();
			postRequest.AddJsonBody(body);

			var response = client.Post<TodoTaskDto>(postRequest);
			Log.StatusCode(log, response, "200,OK");
			var createdTask = (TodoTaskDto)response.Data;

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
			Log.TestTitle(log, TestContext.CurrentContext.Test.Name);
			var auth = new Authentication();
			var token = auth.LogIn();

			//Act
			RestClient client = ApiMain.ClientManager.Client;
			Log.ClientCreation(log);
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
			Log.TestTitle(log, TestContext.CurrentContext.Test.Name);
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
			Log.StatusCode(log, deleteResponce, "200,OK");

			var getRequest = new RestRequest(path, Method.GET);
			var getResponce = client.Get<TodoTaskDto>(getRequest);
			Log.StatusCode(log, getResponce, "500,Internal Server Error");

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
