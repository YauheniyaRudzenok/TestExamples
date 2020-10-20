using System;
using System.Collections.Generic;
using System.Linq;
using Jane.Tests.Infrastructure;
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
		public TaskPage(IWebDriver driver) : base(driver)
		{

		}

		public TaskPage():base()
		{

		}

		protected override string PageURL => Config.Instance["appSettings:webURL"];
		#endregion

		#region Elements

		public IWebElement AboutLinkItem() => Driver.FindElement(By.LinkText("About"));
		public IWebElement LoginButtonItem() => Driver.FindElement(By.CssSelector("a[href$='login'"));
		public IWebElement HomeButtonItem() => Driver.FindElement(By.CssSelector("a[class^='nav-link'"));
		public IWebElement AddTaskButtonItem() => Driver.FindElement(By.CssSelector("a[href$='taskedit'"));
		public IWebElement LogOutButtonItem() => Driver.FindElement(By.CssSelector("a[href$='logout'"));
		public IWebElement LatestCreatedTaskEditButton() => Driver.FindElement(By.XPath("//tr[last()]/td/a[contains(@href, 'taskedit')]"));
		public IWebElement LatestCreatedTaskInfoButton() => Driver.FindElement(By.XPath("//tr[last()]/td/a[contains(@href, 'taskview')]"));

		#endregion

		#region Actions
		public void WaitForPageLoaded()
		{
			var item = "h2";
			WaitByCss(item);
		}

		public IEnumerable<string> ReturnTableHeader()
		{
			var headerList = Driver.FindElements(By.TagName("th"));
			return headerList.Select(i => i.Text);
		}

		public bool ReturnAboutPageLinkText()
		{
			bool AboutLinkTextIsCorrect = AboutLinkItem().Text == GithubLink;
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

		public string ReturnHomeElement() => HomeButtonItem().Text;

		public string ReturnAddTaskElement() => AddTaskButtonItem().Text;

		public string ReturnLogout()=> LogOutButtonItem().Text;

		public string ReturnLogin()=> LoginButtonItem().Text;

		public AddEditTaskPage NavigateToAddTask()
		{
			AddTaskButtonItem().Click();
			return new AddEditTaskPage(Driver);
		}

		public void ClickLogoutButton()
		{
			LogOutButtonItem().Click();
		}

		public List<string> ListOfTasks()
		{
			WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(15));
			var itemList = wait.Until(items => Driver.FindElements(By.XPath(".//tr/td[2]")));
			return itemList.Select(i => i.Text).ToList();
		}

		public IWebElement SearchByTaskTitle(string taskTitle) => Driver.FindElement(By.XPath("//tr/td[text()='" + taskTitle + "']"));

		#endregion

		#region Verification

		public bool EnsureAllHeaderItemsAreDisplayed()
		{
			var headers = ReturnTableHeader();
			return (headers.Contains(TaskStatusColumn)) &&
									(headers.Contains(TaskDescriptionColumn)) &&
									(headers.Contains(TaskCreationColumn)) &&
									(headers.Contains(TaskDueDateColumn));
		}

		public bool EnsureAllMenuItemsAreDisplayed(bool authorized = false, string name = null)
		{
			bool menuItemsArePresented;
			name = name ?? Config.Instance["appCredentials:name"];

			if (authorized)
			{
				menuItemsArePresented = ReturnAddTaskElement() == AddTaskButton &&
										ReturnLogout() == $"{LogoutButton} ({name})";
			}
			else
			{
				menuItemsArePresented = ReturnLogin() == LoginButton;
			}

			menuItemsArePresented &= ReturnHomeElement() == HomeButton;

			return menuItemsArePresented;
		}

		public AddEditTaskPage ClickEditLatestCreatedTask()
		{
			LatestCreatedTaskEditButton().Click();
			return new AddEditTaskPage(Driver);
		}

		public ViewTaskPage ClickInfoForLatestCreatedTask()
		{
			LatestCreatedTaskInfoButton().Click();
			return new ViewTaskPage(Driver);
		}

		#endregion
	}
}
