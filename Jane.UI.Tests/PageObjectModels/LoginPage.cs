using System.Collections.Generic;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class LoginPage:Page
	{
		#region Constants
		private const string NameValidationMessage = "The UserName field is required.";
		private const string PasswordValidationMessage = "The Password field is required.";
		private const string UserNameField = "UserName:";
		private const string PasswordField = "Password:";
		private const string HeaderField = "Login";

		#endregion
		#region Constructors
		public LoginPage(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => Configuration["appSettings:webURL"]+"/login";

		#endregion
		#region Actions
		public string UserName()
		{
			var userNameLable = Driver.FindElement(By.CssSelector("label[for='Input_UserName']")).Text;
			return userNameLable;
		}
		public bool CheckThatLableUserNameLableIsCorrect() => UserName() == UserNameField;

		public string Password()
		{
			var userPasswordLable = Driver.FindElement(By.CssSelector("label[for='Input_Password']")).Text;
			return userPasswordLable;
		}

		public bool CheckThatLablePasswordLableIsCorrect() => Password() == PasswordField;

		public string Header()
		{
			string headerTitle = Driver.FindElement(By.TagName("h1")).Text;
			return headerTitle;
		}
		public bool CheckThatHeaderISValid() => Header() == HeaderField;

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

		public bool CheckTopValidation()
		{
			var listOfMessages = TopValidation();
			bool validationIsCorrect = listOfMessages.Contains(NameValidationMessage) &&
								listOfMessages.Contains(PasswordValidationMessage) &&
								listOfMessages.Count == 2;
			return validationIsCorrect;
		}

		public string RowNameValidationMessage()
		{
			string nameValidation = Driver.FindElement
				(By.CssSelector("span[data-valmsg-for='Input.UserName']")).Text;
			return nameValidation;
		}
		public bool CheckRowNameValidationMessage() => RowNameValidationMessage() == NameValidationMessage;

		public string RowPasswordValidationMessage()
		{
			string passwordValidation = Driver.FindElement
				(By.CssSelector("span[data-valmsg-for='Input.Password']")).Text;
			return passwordValidation;
		}

		public bool CheckRowPasswordValidationMessage() => RowPasswordValidationMessage() == PasswordValidationMessage;

		public void InputUserName(string userName) => Driver.FindElement(By.Id("Input_UserName")).SendKeys(userName);

		public void InputPassword(string password) => Driver.FindElement(By.Id("Input_Password")).SendKeys(password);

		public void NavigateAndLogin(string name = null, string password = null)
		{
			name = name ?? Configuration["appCredentials:name"];
			password = password ?? Configuration["appCredentials:password"];

			NavigateTo();
			InputUserName(name);
			InputPassword(password);
			Submit();
		}
		#endregion
	}
}
