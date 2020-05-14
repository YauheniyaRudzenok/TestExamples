using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class LoginPageFailed:Page
	{
		public LoginPageFailed(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => "http://localhost:63508/loginfailed";

		public string Header()
		{
			var header = Driver.FindElement(By.CssSelector("h5[class='card-title']")).Text;
			return header;
		}

		public string WarningText()
		{
			var text = Driver.FindElement(By.CssSelector("p[class^='card-text'")).Text;
			return text;
		}
	}
}
