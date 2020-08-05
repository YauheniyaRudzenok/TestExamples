using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Jane.UI.Tests.TestServices;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class LoginPageTests
	{

		[Test]
		public void CheckItemsOnThePage()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateTo();

			//Assert
			Assert.IsTrue(loginPage.CheckThatLableUserNameLableIsCorrect());
			Assert.IsTrue(loginPage.CheckThatLablePasswordLableIsCorrect());
			Assert.IsTrue(loginPage.CheckThatHeaderISValid());
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
			Assert.IsTrue(loginPage.CheckTopValidation());
			Assert.That(loginPage.CheckRowPasswordValidationMessage());
			Assert.That(loginPage.CheckRowNameValidationMessage());
		}
		//add submit empty login; submit empty password
		[Test]
		public void SubmitingInvalidData()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateAndLogin(Randoms.GenerateStringValueInRange(1, 100),
				Randoms.GenerateStringValueInRange(1, 100));
			var loginPageFiled = new LoginFailedPage(driver);

			//Assert
			loginPageFiled.EnsurePageLoaded();
			Assert.IsTrue(loginPageFiled.CheckFailedLoginHeader());
			Assert.IsTrue(loginPageFiled.CheckWarningText());
		}

		//add test case for 100 items (that only 100 symbols value is displayed

		[TestCase ("Jane", "Password")]
		[TestCase("jane", "password")]
		[TestCase("JANE", "PASSWORD")]
		public void SubmitingValidData(string name, string password)
		{

			using IWebDriver driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateAndLogin(name, password);
			var taskPage = new TaskPage(driver);

			//Assert
			taskPage.EnsurePageLoaded();

		}

	}
}
