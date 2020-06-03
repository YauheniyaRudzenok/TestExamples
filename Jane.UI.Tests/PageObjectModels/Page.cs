using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public abstract class Page
	{
		public Page()
		{
			Configuration = new Config().BuildConfig();
		}
		protected IWebDriver Driver { get; set; }
		protected virtual string PageURL { get; }
		protected virtual string PageTitle => "Jane.Todo.Web";
		protected IConfigurationRoot Configuration { get; set; }
		
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

	}
}
