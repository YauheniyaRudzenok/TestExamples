using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{
	class TaskPageDefault
	{

		[Test]
		public void ShouldContainHeaderElements()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var taskPage = new TaskPage(driver);
			
			//Act
			taskPage.NavigateTo();
			taskPage.WaitForHeader();

			//Assert
			Assert.That(taskPage.TableHeader(), Contains.Item("Finished"));
			Assert.That(taskPage.TableHeader(), Contains.Item("Title"));
			Assert.That(taskPage.TableHeader(), Contains.Item("Created"));
			Assert.That(taskPage.TableHeader(), Contains.Item("Due Date"));
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Arrange
			using IWebDriver driver = new ChromeDriver();
			var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForHeader();

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
			taskPage.WaitForHeader();
			taskPage.NavigateToLogin();

			//Assert
			loginPage.EnsurePageLoaded();
		}
	}
}
