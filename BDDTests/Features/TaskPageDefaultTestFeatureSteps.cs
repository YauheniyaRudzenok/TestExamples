//using Jane.Tests.Infrastructure;
//using Jane.UI.Tests.PageObjectModels;
//using Microsoft.Extensions.Configuration;
//using OpenQA.Selenium;
//using TechTalk.SpecFlow;

//namespace BDDTests.Features
//{
//	[Binding]
//    public class TaskPageDefaultTestFeatureSteps
//    {
//        private IConfigurationRoot configuration;
//        private IWebDriver driver;
//        TaskPage taskPage;
//        AboutPage aboutPage;
//        LoginPage loginPage;

//        [Given(@"user is on Home page")]
//        public void GivenUserIsOnHomePage()
//        {
//            configuration = Config.Instance;
//            driver = BrowserFabric.CreateDriver(configuration["browserSettings:browser"]);
//            taskPage = new TaskPage();
//            loginPage = new LoginPage();
//            taskPage.NavigateTo();
//            taskPage.WaitForPageLoaded();
//        }
        
//        [When(@"he clicks Log In page")]
//        public void WhenHeClickLogInPage()
//        {
//            taskPage.NavigateToLogin();
//        }

//        [When(@"he clicks About page")]
//        public void WhenHeClicksAboutPage()
//        {
//            aboutPage = taskPage.ClickAboutLink();
//        }

//        [Then(@"Log in page is opened")]
//        public void ThenLogInPageIsOpened()
//        {
//            loginPage.EnsurePageLoaded();
//            driver.Dispose();
//        }

//        [Then(@"About page is opened")]
//        public void ThenAboutPageIsOpened()
//        {
//            driver.SwitchTo().Window(driver.WindowHandles[1]);
//            aboutPage.EnsurePageLoaded();
//            driver.Dispose();
//        }
//    }
//}
