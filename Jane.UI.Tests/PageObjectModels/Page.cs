using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Jane.Tests.Infrastructure;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	public abstract class Page
	{
		public Page()
		{
			Configuration = Config.Instance;
		}
		protected IWebDriver Driver { get; set; }
		protected virtual string PageURL { get; }
		protected virtual string PageTitle => "Jane.Todo.Web";
		protected IConfigurationRoot Configuration { get; private set; }
		
		public void NavigateTo()
		{
			Driver.Navigate().GoToUrl(PageURL);
			EnsurePageLoaded();
		}

		public void EnsurePageLoaded(bool onlyCheckStartsWith = true)
		{
			bool URLIsCorrect;
			if (onlyCheckStartsWith)
			{
				URLIsCorrect = Driver.Url.StartsWith(PageURL);
			}
			else
			{
				URLIsCorrect = Driver.Url == PageURL;
			}
			bool PageIsLoaded = URLIsCorrect && (Driver.Title==PageTitle);
			if (!PageIsLoaded)
			{
				throw new Exception($"Failed to load the page. Page {Driver.Url} with title {Driver.Title} is not loaded");
			}
		}

		public string JavaScriptExecutor(string script)
		{
			IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)Driver;
			return javaScriptExecutor.ExecuteScript(script)?.ToString();
		}

		public void WaitByCss(string item=null)
		{
			var script = "return document.readyState";
			WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(30));
			wait.Until(condition => JavaScriptExecutor(script).Equals("complete"));
			if(item!=null)
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(item)));
		}

	}
}
