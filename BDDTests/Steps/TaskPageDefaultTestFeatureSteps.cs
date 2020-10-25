//using Jane.Tests.Infrastructure;
//using Jane.UI.Tests.PageObjectModels;
//using Microsoft.Extensions.Configuration;
//using NUnit.Framework;
//using OpenQA.Selenium;
//using TechTalk.SpecFlow;

//namespace BDDTests.Steps
//{

//	[Binding]
//	public class NavigateToLoginPage:Main
//	{

//		[When(@"he clicks Log In link")]
//		public void ClickLogIn()
//		{
//			taskPage.NavigateTo();
//			taskPage.WaitForPageLoaded();
//			loginPage = taskPage.NavigateToLogin();
//		}

//		[Then(@"Log in page is opened")]
//		public void LogInPageIsOpened()
//		{
//			loginPage.EnsurePageLoaded();
//			loginPage.Dispose();
//		}
//	}

//	[Binding]
//	public class NavigateToAboutPage:Main
//	{
//		[When(@"he clicks About link")]
//		public void NavigateToGit()
//		{
//			aboutPage = taskPage.ClickAboutLink();
//			aboutPage.Driver.SwitchTo().Window(aboutPage.Driver.WindowHandles[1]);
//		}

//		[Then(@"About page is opened")]
//		public void AssertAboutPageIsLoaded()
//		{
//			aboutPage.EnsurePageLoaded();
//			aboutPage.Dispose();
//		}
//	}

//	public class Main
//	{
//		public TaskPage taskPage;
//		public LoginPage loginPage;
//		public AboutPage aboutPage;

//		[Given(@"user is on Home page")]
//		public void GoToTaskPage()
//		{
//			taskPage = new TaskPage();
//		}

//	}
//}
