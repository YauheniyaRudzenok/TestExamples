using System;
using Jane.Tests.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	public class LoginFailedPage:Page
	{
		#region Constants
		private const string FailedLoginHeader = "Login failed";
		private const string FailedLoginText = "It looks like you aren't Jane and you didn't use 'password' to authenticate yourself.";

		#endregion
		#region Constructors

		public LoginFailedPage(IWebDriver driver): base(driver)
		{

		}
		public LoginFailedPage() : base()
		{

		}
		protected override string PageURL => Config.Instance["appSettings:webURL"] +"/loginfailed";
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

		#endregion

		#region Verification

		public bool CheckWarningText() => WarningText() == FailedLoginText;

		public bool CheckFailedLoginHeader() => HeaderText() == FailedLoginHeader;

		#endregion
	}
}
