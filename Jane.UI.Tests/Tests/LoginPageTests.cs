using Jane.Tests.Infrastructure;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class LoginPageTests
	{
		private PageManager pageManager;

		[SetUp]
		public void CreatePageManager()
        {
			pageManager = new PageManager();
		}

		[Test]
		public void CheckItemsOnThePage()
		{
			//Act
			pageManager.LoginPage.NavigateTo();
			pageManager.LoginPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
			Assert.IsTrue(pageManager.LoginPage.CheckThatLableUserNameLableIsCorrect());
			Assert.IsTrue(pageManager.LoginPage.CheckThatLablePasswordLableIsCorrect());
			Assert.IsTrue(pageManager.LoginPage.CheckThatHeaderISValid());
		}

		[Test]
		public void SubmitEmptyValues()
		{
			//Act
			pageManager.LoginPage.NavigateTo();
			pageManager.LoginPage.Submit();
			pageManager.LoginPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
			Assert.IsTrue(pageManager.LoginPage.CheckTopValidation());
			Assert.That(pageManager.LoginPage.CheckRowPasswordValidationMessage());
			Assert.That(pageManager.LoginPage.CheckRowNameValidationMessage());
		}
		//add submit empty login; submit empty password
		[Test]
		public void SubmitingInvalidData()
		{
			//Act
			pageManager.LoginPage.NavigateAndLogin(Randoms.GenerateStringValueInRange(1, 100),
				Randoms.GenerateStringValueInRange(1, 100));
			pageManager.LoginFailedPage.EnsurePageLoaded();
			pageManager.LoginFailedPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
			Assert.IsTrue(pageManager.LoginFailedPage.CheckFailedLoginHeader());
			Assert.IsTrue(pageManager.LoginFailedPage.CheckWarningText());
		}

		//add test case for 100 items (that only 100 symbols value is displayed

		[TestCase ("Jane", "Password")]
		[TestCase("jane", "password")]
		[TestCase("JANE", "PASSWORD")]
		public void SubmitingValidData(string name, string password)
		{
			//Act
			pageManager.LoginPage.NavigateAndLogin(name, password);
			pageManager.LoginPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
			pageManager.TaskPage.EnsurePageLoaded();
		}

		[TearDown]
		public void TearDown()
		{
			pageManager.CleanWebDriver();
		}
	}
}
