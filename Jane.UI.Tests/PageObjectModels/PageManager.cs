using System;
using System.Collections.Generic;
using System.Text;
using Jane.Tests.Infrastructure;
using Jane.UI.Tests.PageObjectModels;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class PageManager
	{
		private readonly IWebDriver driver;
		private AboutPage _aboutPage;
		private AddEditTaskPage _addEditTaskPage;
		private LoginFailedPage _loginFailedPage;
		private LoginPage _loginPage;
		private TaskPage _taskPage;
		private ViewTaskPage _viewTaskPage;

		public PageManager()
		{
			driver = BrowserFabric.CreateDriver(Config.Instance["browserSettings:browser"]);
		}

		public AboutPage AboutPage => _aboutPage ??= new AboutPage(driver);
		public AddEditTaskPage AddEditTaskPage => _addEditTaskPage ??= new AddEditTaskPage(driver);
		public LoginFailedPage LoginFailedPage => _loginFailedPage ??= new LoginFailedPage(driver);
		public LoginPage LoginPage => _loginPage ??= new LoginPage(driver);
		public TaskPage TaskPage => _taskPage ??= new TaskPage(driver);
		public ViewTaskPage ViewTaskPage => _viewTaskPage ??= new ViewTaskPage(driver);

		public void Clean()
		{
			driver?.Dispose();
		}
	}
}
