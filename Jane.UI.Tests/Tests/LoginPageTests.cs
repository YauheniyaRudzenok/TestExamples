using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class LoginPageTests
	{
		[Test]
		public void CheckItemsOnThePage()
		{
			//Act
			Main.PageManager.LoginPage.NavigateTo();

			//Assert
			Assert.IsTrue(Main.PageManager.LoginPage.CheckThatLableUserNameLableIsCorrect());
			Assert.IsTrue(Main.PageManager.LoginPage.CheckThatLablePasswordLableIsCorrect());
			Assert.IsTrue(Main.PageManager.LoginPage.CheckThatHeaderISValid());
		}

		[Test]
		public void SubmitEmptyValues()
		{
			//Act
			Main.PageManager.LoginPage.NavigateTo();
			Main.PageManager.LoginPage.Submit();

			//Assert
			Assert.IsTrue(Main.PageManager.LoginPage.CheckTopValidation());
			Assert.That(Main.PageManager.LoginPage.CheckRowPasswordValidationMessage());
			Assert.That(Main.PageManager.LoginPage.CheckRowNameValidationMessage());
		}
		//add submit empty login; submit empty password
		[Test]
		public void SubmitingInvalidData()
		{
			//Act
			Main.PageManager.LoginPage.NavigateAndLogin(Randoms.GenerateStringValueInRange(1, 100),
				Randoms.GenerateStringValueInRange(1, 100));
			Main.PageManager.LoginFailedPage.EnsurePageLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.LoginFailedPage.CheckFailedLoginHeader());
			Assert.IsTrue(Main.PageManager.LoginFailedPage.CheckWarningText());
		}

		//add test case for 100 items (that only 100 symbols value is displayed

		[TestCase ("Jane", "Password")]
		[TestCase("jane", "password")]
		[TestCase("JANE", "PASSWORD")]
		public void SubmitingValidData(string name, string password)
		{
			//Act
			Main.PageManager.LoginPage.NavigateAndLogin(name, password);

			//Assert
			Main.PageManager.TaskPage.EnsurePageLoaded();
		}

		[TearDown]
		public void TearDown()
		{
			Main.Clean();
		}
	}
}
