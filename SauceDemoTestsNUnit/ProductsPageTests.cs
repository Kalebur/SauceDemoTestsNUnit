using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoTestsNUnit
{
    public class ProductsPageTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private ProductsPage _productsPage;

        [SetUp]
        public void Setup() {
            _driver = new ChromeDriver();
            _loginPage = new LoginPage(_driver);
            _productsPage = new ProductsPage(_driver);
        }

        [Test]
        public void ProductsPage_DisplaysAtLeastOneProduct_ToLoggedInUsers()
        {
            _loginPage.LoginAs("standard_user");
            var itemCount = _productsPage.InventoryItems.Count;

            Assert.That(itemCount, Is.AtLeast(1));
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
