using System.Collections.Generic;
using System.Linq;
using Jane.Tests.Infrastructure;
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
		public LoginPage(IWebDriver driver):base(driver)
		{

		}
		protected override string PageURL => Config.Instance["appSettings:webURL"]+"/login";

		#endregion
		#region Actions
		public string ReturnUserName()
		{
			var userNameLable = Driver.FindElement(By.CssSelector("label[for='Input_UserName']")).Text;
			return userNameLable;
		}
		
		public string ReturnPassword()
		{
			var userPasswordLable = Driver.FindElement(By.CssSelector("label[for='Input_Password']")).Text;
			return userPasswordLable;
		}

		public string ReturnHeader()
		{
			string headerTitle = Driver.FindElement(By.TagName("h1")).Text;
			return headerTitle;
		}

		public void Submit() => Driver.FindElement(By.CssSelector("button[type='submit']")).Click();

		public List<string> TopValidation()
		{
			var messagesObjects = Driver.FindElements(By.CssSelector("ul>li"));
			return messagesObjects.Select(i=>i.Text).ToList();
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

		public void NavigateAndLogin(string name = null, string password = null)
		{
			name = name ?? Config.Instance["appCredentials:name"];
			password = password ?? Config.Instance["appCredentials:password"];

			NavigateTo();
			InputUserName(name);
			InputPassword(password);
			Submit();
		}

		#endregion

		#region Verification

		public bool CheckThatLablePasswordLableIsCorrect() => ReturnPassword() == PasswordField;

		public bool CheckThatLableUserNameLableIsCorrect() => ReturnUserName() == UserNameField;

		public bool CheckRowPasswordValidationMessage() => RowPasswordValidationMessage() == PasswordValidationMessage;

		public bool CheckRowNameValidationMessage() => RowNameValidationMessage() == NameValidationMessage;

		public bool CheckTopValidation()
		{
			var listOfMessages = TopValidation();
			bool validationIsCorrect = listOfMessages.Contains(NameValidationMessage) &&
								listOfMessages.Contains(PasswordValidationMessage) &&
								listOfMessages.Count == 2;
			return validationIsCorrect;
		}

		public bool CheckThatHeaderISValid() => ReturnHeader() == HeaderField;

		#endregion
	}
}
