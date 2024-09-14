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
        public IWebElement ErrorLabel => _driver.FindElement(By.XPath("//div[@class='error-message-container error']//h3"));

        public void LoginAs(string username, string password = "secret_sauce")
        {
            _driver.Navigate().GoToUrl(Url);
            UsernameTextbox.SendKeys(username);
            PasswordTextbox.SendKeys(password);
            LoginButton.Click();
        }
    }
}
