﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class Page
	{
		protected IWebDriver Driver;
		protected virtual string PageURL { get; }
		protected virtual string PageTitle { get; }

		public void NavigateTo()
		{
			Driver.Navigate().GoToUrl(PageURL);
			EnsurePageLoaded();
		}

		public void EnsurePageLoaded()
		{
			bool PageIsLoaded = (Driver.Url == PageURL)&&(Driver.Title==PageTitle);
			if (!PageIsLoaded)
			{
				throw new Exception($"Failed to load the page. Page {Driver.Url} with title {Driver.Title} is not loaded");
			}
		}

	}
}
