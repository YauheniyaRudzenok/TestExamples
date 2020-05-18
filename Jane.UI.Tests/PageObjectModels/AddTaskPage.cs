using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class AddTaskPage:Page
	{
		public AddTaskPage(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => "http://localhost:63508/taskedit";
	}
}
