using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class LoginPage:Page
	{
		public LoginPage(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => "http://localhost:63508/login";

		public string UserName()
		{
			var userNameLable = Driver.FindElement(By.CssSelector("label[for='Input_UserName']")).Text;
			return userNameLable;
		}

		public string Password()
		{
			var userPasswordLable = Driver.FindElement(By.CssSelector("label[for='Input_Password']")).Text;
			return userPasswordLable;
		}

		public string Header()
		{
			string headerTitle = Driver.FindElement(By.TagName("h1")).Text;
			return headerTitle;
		}

		public void Submit() => Driver.FindElement(By.CssSelector("button[type='submit']")).Click();

		public List<string> TopValidation()
		{
			var messagesObjects = Driver.FindElements(By.CssSelector("ul>li"));
			List<string> messages = new List<string>();
			foreach (IWebElement item in messagesObjects)
			{
				messages.Add(item.Text);
			}
			return messages;
		}

		public string RowNameValidationMessage()
		{
			string nameValidation = Driver.FindElement
				(By.CssSelector("span[data-valmsg-for='Input.UserName']")).Text;
			return nameValidation;
		}

		public string RowPasswordValidationMessage()
		{
			string passwordValidation = Driver.FindElement
				(By.CssSelector("span[data-valmsg-for='Input.Password']")).Text;
			return passwordValidation;
		}

		public void InputUserName(string userName) => Driver.FindElement(By.Id("Input_UserName")).SendKeys(userName);

		public void InputPassword(string password) => Driver.FindElement(By.Id("Input_Password")).SendKeys(password);

		public void NavigateAndLogin(string name, string password)
		{
			NavigateTo();
			InputUserName(name);
			InputPassword(password);
			Submit();
		}
	}
}
