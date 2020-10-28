using System;
using System.Collections;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using TechTalk.SpecFlow;

namespace BDDTests.Steps
{
	[Binding]
	public class CommonSteps
	{
		[After]
		public void CleanUp()
		{
			Main.Clean();
		}

		[Given(@"I am on Home page")]
		public void GoToTaskPage()
		{
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
		}

		[Given(@"I navigate and login with (.*) login and (.*) password")]
		public void FillInCredsAndSubmit(string login, string password)
		{
			if (login == "random")
			{
				login = Randoms.GenerateStringValueInRange(1, 100);
			}
			if (password == "random")
			{
				password = Randoms.GenerateStringValueInRange(1, 100);
			}
			Main.PageManager.LoginPage.NavigateAndLogin(login, password);
		}

		[Given (@"I wait for (.*) page to be loaded")]
		public void WaitForPageLoaded(string page)
		{
			switch (page)
			{
				case "TaskPage":
					Main.PageManager.TaskPage.WaitForPageLoaded();
					Main.PageManager.TaskPage.EnsurePageLoaded();
					break;
				case "LoginFailedPage":
					Main.PageManager.LoginFailedPage.WaitForPageLoaded();
					Main.PageManager.LoginFailedPage.EnsurePageLoaded();
					break;
			}
		}
	}
}
