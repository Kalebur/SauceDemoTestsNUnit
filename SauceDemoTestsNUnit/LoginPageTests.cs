using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Diagnostics;

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
            _loginPage.LoginAs(username);
            var productsTitleVisible = _loginPage.ProductsTitle.Displayed;

            Assert.That(productsTitleVisible, Is.True);
        }

        [Test]
        public void LoginPage_DisplaysLockedOutError_WhenLoggingInAsLockedUser()
        {
            var expectedErrorText = "user has been locked out";

            _loginPage.LoginAs("locked_out_user");
            var lockedOutErrorDisplayed = _loginPage.ErrorLabel.Displayed;
            var lockedOutErrorText = _loginPage.ErrorLabel.Text;
            var errorMessageContainsExpectedText = lockedOutErrorText.Contains(expectedErrorText);

            Assert.That((lockedOutErrorDisplayed && errorMessageContainsExpectedText), Is.True);
        }

        [TestCase("standard_user")]
        [TestCase("problem_user")]
        [TestCase("performance_glitch_user"), DisplayName("performance_glitch_user")]
        [TestCase("error_user")]
        [TestCase("visual_user")]
        public void LoginPage_RedirectsToProductPageInLessThanThreeSeconds_AfterSuccessfulLogin(string username)
        {
            var maxLoadTime = TimeSpan.FromSeconds(3);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));

            Stopwatch stopwatch = Stopwatch.StartNew();
            _loginPage.LoginAs(username);
            wait.Until(_driver => _loginPage.ProductsTitle.Displayed);
            var actualLoadingTime = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
            
            Assert.That(actualLoadingTime, Is.AtMost(maxLoadTime));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}