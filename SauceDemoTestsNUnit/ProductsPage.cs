using OpenQA.Selenium;

namespace SauceDemoTestsNUnit
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;

        public ProductsPage(IWebDriver driver) {
            _driver = driver;
        }

        public string Url = "https://www.saucedemo.com/inventory.html";

        public IWebElement InventoryList => _driver.FindElement(By.Id("inventory_container"));
        public IList<IWebElement> InventoryItems => InventoryList.FindElements(By.XPath(".//div[@class='inventory_item']"));
    }
}
