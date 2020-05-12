using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{
	public class LoginPageTest
	{
		private const string NameValidationMessage = "The UserName field is required.";
		private const string PasswordValidationMessage = "The Password field is required.";

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
		public void SubmitingInValidData()
		{
			using IWebDriver driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);
			loginPage.GenerateStringValueInRange(50, 100);
		}

		//[Test]
		//public void SubmitingValidData()
		//{
		//	const string name = "Jane";
		//	const string password = "Password";

		//	using IWebDriver driver = new ChromeDriver();
		//	var loginPage = new LoginPage(driver);

		//	//Act
		//	loginPage.NavigateTo();

		//}

	}
}
