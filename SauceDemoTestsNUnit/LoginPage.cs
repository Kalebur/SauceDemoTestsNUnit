using OpenQA.Selenium;

namespace SauceDemoTestsNUnit
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver) {
            _driver = driver;
        }

        public string Url = "https://www.saucedemo.com/";

        public IWebElement UsernameTextbox => _driver.FindElement(By.Id("user-name"));
        public IWebElement PasswordTextbox => _driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        public IWebElement ProductsTitle => _driver.FindElement(By.XPath("//span[@class='title']"));
    }
}
