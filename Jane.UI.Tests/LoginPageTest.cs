using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Jane.UI.Tests.TestServices;

namespace Jane.UI.Tests
{
	public class LoginPageTest
	{
		private const string NameValidationMessage = "The UserName field is required.";
		private const string PasswordValidationMessage = "The Password field is required.";
		private const string FailedLoginHeader = "Login failed";
		private const string FailedLoginText = "It looks like you aren't Jane and you didn't use 'password' to authenticate yourself.";

		[Test]
		public void CheckItemsOnThePage()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateTo();

			//Assert
			Assert.That(loginPage.UserName(), Is.EqualTo("UserName:"));
			Assert.That(loginPage.Password(), Is.EqualTo("Password:"));
			Assert.That(loginPage.Header(), Is.EqualTo("Login"));
		}

		[Test]
		public void SubmitEmptyValues()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateTo();
			loginPage.Submit();

			//Assert
			Assert.That(loginPage.TopValidation(), Contains.Item(NameValidationMessage));
			Assert.That(loginPage.TopValidation(), Contains.Item(PasswordValidationMessage));
			Assert.That(loginPage.RowNameValidationMessage(), Is.EqualTo(NameValidationMessage));
			Assert.That(loginPage.RowPasswordValidationMessage(), Is.EqualTo(PasswordValidationMessage));
		}

		[Test]
		public void SubmitingInvalidData()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var input = new TestService();
			var loginPage = new LoginPage(driver);
			var loginPageFiled = new LoginPageFailed(driver);
			loginPage.NavigateTo();

			//Act
			loginPage.InputUserName(input.GenerateStringValueInRange(1, 100));
			loginPage.InputPassword(input.GenerateStringValueInRange(1, 20));
			loginPage.Submit();

			//Assert
			loginPageFiled.EnsurePageLoaded();
			Assert.That(loginPageFiled.Header, Is.EqualTo(FailedLoginHeader));
			Assert.That(loginPageFiled.WarningText, Is.EqualTo(FailedLoginText));
		}

		//add test case for 100 items (that only 100 symbols value is displayed

		[Test]
		[TestCase ("Jane", "Password")]
		[TestCase("jane", "password")]
		[TestCase("JANE", "PASSWORD")]
		public void SubmitingValidData(string name, string password)
		{

			using IWebDriver driver = new ChromeDriver();
			var taskPage = new TaskPage(driver);
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateTo();
			loginPage.InputUserName(name);
			loginPage.InputPassword(password);
			loginPage.Submit();

			//Assert
			taskPage.EnsurePageLoaded();
		}

	}
}
