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

		protected override string PageTitle => "Jane.Todo.Web";
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

		public string GenerateStringValueInRange(int min, int max)
		{
			StringBuilder stringBuilder = new StringBuilder();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random(chars.Length);

			var length = random.Next(min, max);

			for (int i =0; i==length; i++)
			{
				stringBuilder.Append(chars[random.Next()]);
			}
			return stringBuilder.ToString();
		}
	}
}
