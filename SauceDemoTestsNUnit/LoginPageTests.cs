using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoTestsNUnit
{
    public class LoginPageTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _loginPage = new LoginPage(_driver);
        }

        [TestCase("standard_user")]
        [TestCase("problem_user")]
        [TestCase("performance_glitch_user")]
        [TestCase("error_user")]
        [TestCase("visual_user")]
        public void LoginPage_RedirectsToAProductsPage_WhenCorrectlyLoggingIn(string username)
        {
            var password = "secret_sauce";

            _driver.Navigate().GoToUrl(_loginPage.Url);
            _loginPage.UsernameTextbox.SendKeys(username);
            _loginPage.PasswordTextbox.SendKeys(password);
            _loginPage.LoginButton.Click();
            var productsTitleVisible = _loginPage.ProductsTitle.Displayed;

            Assert.IsTrue(productsTitleVisible);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}