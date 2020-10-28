using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BDDTests.Steps
{

	[Binding]
	public class LoginWithEmptyValues
	{
		[Given(@"I am on Login Page")]
		public void GoToLoginPage()
		{
			Main.PageManager.LoginPage.NavigateTo();
		}

		[When(@"I click Submit")]
		public void ClickSybmitButton()
		{
			Main.PageManager.LoginPage.Submit();
		}

		[Then(@"I see validation message on the top")]
		public void TopValidation()
		{
			Assert.IsTrue(Main.PageManager.LoginPage.CheckTopValidation());
		}

		[Then(@"I see password validation")]
		public void PasswordRowValidation()
		{
			Assert.That(Main.PageManager.LoginPage.CheckRowPasswordValidationMessage());
		}

		[Then(@"I see login validation")]
		public void LoginRowValidation()
		{
			Assert.That(Main.PageManager.LoginPage.CheckRowNameValidationMessage());
		}
	}

	[Binding]
	public class LoginWithInvalidData
	{
		[Then(@"I see header validation")]
		public void HeaderVelidationForIncorrectValue()
		{
			Assert.IsTrue(Main.PageManager.LoginFailedPage.CheckFailedLoginHeader());
		}

		[Then(@"I see invalid data warning message")]
		public void InvalidDataWarning()
		{
			Assert.IsTrue(Main.PageManager.LoginFailedPage.CheckWarningText());
		}
	}

	[Binding]
	public class LoginWithCorrectValues
	{

		[Then(@"I am redirected to Tasks Page")]
		public void EnsureHomePageIsLoaded()
		{
			Main.PageManager.TaskPage.EnsurePageLoaded();
		}
	}
}