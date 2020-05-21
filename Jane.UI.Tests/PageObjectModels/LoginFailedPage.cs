using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class LoginFailedPage:Page
	{
		#region Constants
		private const string FailedLoginHeader = "Login failed";
		private const string FailedLoginText = "It looks like you aren't Jane and you didn't use 'password' to authenticate yourself.";

		#endregion
		#region Constructors
		public LoginFailedPage(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => "http://localhost:63508/loginfailed";
		#endregion
		#region Actions
		public string HeaderText()
		{
			var header = Driver.FindElement(By.CssSelector("h5[class='card-title']")).Text;
			return header;
		}

		public string WarningText()
		{
			var text = Driver.FindElement(By.CssSelector("p[class^='card-text'")).Text;
			return text;
		}

		public bool CheckFailedLoginHeader() => HeaderText() == FailedLoginHeader;

		public bool CheckWarningText() => WarningText() == FailedLoginText;
		#endregion
	}
}
