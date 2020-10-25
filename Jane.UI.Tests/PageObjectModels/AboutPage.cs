using OpenQA.Selenium;

namespace Jane.UI.Tests.PageObjectModels
{
	public class AboutPage: Page
	{
		#region Constructors
		public AboutPage(IWebDriver driver):base(driver)
		{

		}
		protected override string PageURL => "https://github.com/YauheniyaRudzenok";
		protected override string PageTitle => "YauheniyaRudzenok · GitHub";
        #endregion
    }
}
