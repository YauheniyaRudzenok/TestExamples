using Jane.UI.Tests.PageObjectModels;
using TechTalk.SpecFlow;

namespace BDDTests.Steps
{

	[Binding]
	public class NavigateToLoginPage
	{

		[When(@"I click Log In link")]
		public void ClickLogIn()
		{
			Main.PageManager.TaskPage.NavigateToLogin();
		}

		[Then(@"I see log in page is opened")]
		public void LogInPageIsOpened()
		{
			Main.PageManager.LoginPage.EnsurePageLoaded();
		}
	}

	[Binding]
	public class NavigateToAboutPage
	{
		[When(@"I click About link")]
		public void NavigateToGit()
		{
			Main.PageManager.TaskPage.ClickAboutLink();
			Main.PageManager.SwitchTab(1);
		}

		[Then(@"I see About page is opened")]
		public void AssertAboutPageIsLoaded()
		{
			Main.PageManager.AboutPage.EnsurePageLoaded();
		}
	}
}
