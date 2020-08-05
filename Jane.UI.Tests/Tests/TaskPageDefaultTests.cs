using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	class TaskPageDefaultTests
	{

		[Test]
		public void ShouldContainAllElements()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var taskPage = new TaskPage(driver);
			
			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(taskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(taskPage.EnsureAllMenuItemsAreDisplayed());
			Assert.IsTrue(taskPage.ReturnAboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();

			//WebDriverWait wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(30));
			//wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href*='github']")));
			//string script = @"document.querySelectorAll('a[href *=""github""]');";
			//IJavaScriptExecutor execute = (IJavaScriptExecutor)driver;
			//var obj = execute.ExecuteScript(script);

			AboutPage aboutPage = taskPage.ClickAboutLink();
			driver.SwitchTo().Window(driver.WindowHandles[1]);

			//Assert
			aboutPage.EnsurePageLoaded();
		}

		[Test]
		public void SignInLinkNavigation()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var taskPage = new TaskPage(driver);
			var loginPage = new LoginPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();
			taskPage.NavigateToLogin();

			//Assert
			loginPage.EnsurePageLoaded();
		}
	}
}
