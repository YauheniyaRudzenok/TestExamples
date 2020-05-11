using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class AboutPage: Page
	{
		public AboutPage(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => "https://github.com/YauheniyaRudzenok";
		protected override string PageTitle => "YauheniyaRudzenok · GitHub";

	}
}
