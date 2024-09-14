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
        public void LoginPage_RedirectsToAProductsPage_WhenCorrectlyLoggingIn(string username)
        {
            var password = "secret_sauce";


        }
    }
}