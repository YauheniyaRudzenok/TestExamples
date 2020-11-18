using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class LoginPageTests
	{
		private PageManager PageManager;

		[SetUp]
		public void CreatePageManager()
        {
			PageManager = new PageManager();
		}

		[Test]
		public void CheckItemsOnThePage()
		{
			//Act
			PageManager.LoginPage.NavigateTo();

			//Assert
			Assert.IsTrue(PageManager.LoginPage.CheckThatLableUserNameLableIsCorrect());
			Assert.IsTrue(PageManager.LoginPage.CheckThatLablePasswordLableIsCorrect());
			Assert.IsTrue(PageManager.LoginPage.CheckThatHeaderISValid());
		}

		[Test]
		public void SubmitEmptyValues()
		{
			//Act
			PageManager.LoginPage.NavigateTo();
			PageManager.LoginPage.Submit();

			//Assert
			Assert.IsTrue(PageManager.LoginPage.CheckTopValidation());
			Assert.That(PageManager.LoginPage.CheckRowPasswordValidationMessage());
			Assert.That(PageManager.LoginPage.CheckRowNameValidationMessage());
		}
		//add submit empty login; submit empty password
		[Test]
		public void SubmitingInvalidData()
		{
			//Act
			PageManager.LoginPage.NavigateAndLogin(Randoms.GenerateStringValueInRange(1, 100),
				Randoms.GenerateStringValueInRange(1, 100));
			PageManager.LoginFailedPage.EnsurePageLoaded();

			//Assert
			Assert.IsTrue(PageManager.LoginFailedPage.CheckFailedLoginHeader());
			Assert.IsTrue(PageManager.LoginFailedPage.CheckWarningText());
		}

		//add test case for 100 items (that only 100 symbols value is displayed

		[TestCase ("Jane", "Password")]
		[TestCase("jane", "password")]
		[TestCase("JANE", "PASSWORD")]
		public void SubmitingValidData(string name, string password)
		{
			//Act
			PageManager.LoginPage.NavigateAndLogin(name, password);

			//Assert
			PageManager.TaskPage.EnsurePageLoaded();
		}

		[TearDown]
		public void TearDown()
		{
			PageManager.CleanWebDriver();
		}
	}
}
