using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	public class TaskPage:Page
	{
		#region Constants
		private const string TaskStatusColumn= "Finished";
		private const string TaskDescriptionColumn = "Title";
		private const string TaskCreationColumn = "Created";
		private const string TaskDueDateColumn = "Due Date";
		private const string AddTaskButton = "Add task";
		private const string LogoutButton = "Log out";
		private const string LoginButton = "Log in";
		private const string HomeButton = "Home";
		private const string GithubLink = "About";
		#endregion

		#region Constructors
		public TaskPage (IWebDriver driver)
		{
			Driver = driver;
		}

		protected override string PageURL => Configuration["appSettings:webURL"];
		#endregion

		#region Elements

		public IWebElement AboutLinkItem() => Driver.FindElement(By.CssSelector("a[href*='github']"));
		public IWebElement LoginButtonItem() => Driver.FindElement(By.CssSelector("a[href$='login'"));
		public IWebElement HomeButtonItem() => Driver.FindElement(By.CssSelector("a[class^='nav-link'"));
		public IWebElement AddTaskButtonItem() => Driver.FindElement(By.CssSelector("a[href$='taskedit'"));
		public IWebElement LogOutButtonItem() => Driver.FindElement(By.CssSelector("a[href$='logout'"));

		#endregion

		#region Actions
		public void WaitForPageLoaded()
		{
			var script = "return document.readyState";
			IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)Driver;
			WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(30));
			wait.Until(condition=>javaScriptExecutor.ExecuteScript(script).Equals("complete"));
		}

		public List<string> TableHeader()
		{
			var headerList = Driver.FindElements(By.TagName("th"));
			var headers = new List<string>();
			foreach (IWebElement item in headerList)
			{
				headers.Add(item.Text);
			}
			return headers;
		}

		public bool AboutPageLinkText()
		{
			bool AboutLinkTextIsCorrect = true;
			AboutLinkTextIsCorrect= AboutLinkItem().Text== GithubLink;
			return AboutLinkTextIsCorrect;
		}

		public AboutPage ClickAboutLink()
		{
			AboutLinkItem().Click();
			return new AboutPage(Driver);
		}

		public LoginPage NavigateToLogin()
		{
			LoginButtonItem().Click();
			return new LoginPage(Driver);
		}

		public string HomeElement() => HomeButtonItem().Text;

		public string AddTaskElement() => AddTaskButtonItem().Text;
		public string Logout()=> LogOutButtonItem().Text;

		public string Login() => LoginButtonItem().Text;

		public bool EnsureAllHeaderItemsAreDisplayed()
		{
			bool headerItemsArePresented = true;
			var headers = TableHeader();
			headerItemsArePresented = (headers.Contains(TaskStatusColumn)) &&
									(headers.Contains(TaskDescriptionColumn)) &&
									(headers.Contains(TaskCreationColumn)) &&
									(headers.Contains(TaskDueDateColumn));
			return headerItemsArePresented;
		}

		public bool EnsureAllMenuItemsAreDisplayed(bool authorized=false, string name="Jane")
		{
			bool menuItemsArePresented=true;

			if (authorized)
			{
				menuItemsArePresented = AddTaskElement() == AddTaskButton &&
										Logout() == $"{LogoutButton} ({name})";
			}
			else
			{
				menuItemsArePresented = Login() == LoginButton;
			}

			menuItemsArePresented &= HomeElement() == HomeButton;

			return menuItemsArePresented;
		}

		public AddEditTaskPage NavigateToAddTask()
		{
			AddTaskButtonItem().Click();
			return new AddEditTaskPage(Driver);
		}

		public void ClickLogoutButton()
		{
			LogOutButtonItem().Click();
		}
		#endregion
	}
}
