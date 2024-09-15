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
        public void Setup()
        {
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

        [Test]
        public void ProductsPage_ContainsAPrice_ForEachProduct()
        {
            _loginPage.LoginAs("standard_user");
            var pricesIncludedForAllProducts = true;

            foreach (var product in _productsPage.InventoryItems)
            {
                var priceText = string.Join("",
                    product.FindElement(By.CssSelector(".inventory_item_price")).Text.Skip(1));
                var priceIncluded = decimal.TryParse(priceText, out decimal price);
                if (!(priceIncluded && price >= 0))
                {
                    pricesIncludedForAllProducts = false;
                    break;
                }
            }

            Assert.That(pricesIncludedForAllProducts, Is.True);
        }

        [Test]
        public void ProductsPage_ContainsAnAddToCartButton_ForEachProduct()
        {
            _loginPage.LoginAs("standard_user");
            var addToCartButtonForEachProduct = true;

            foreach (var product in _productsPage.InventoryItems)
            {
                try
                {
                    var addToCartButton = product.FindElement(By.CssSelector(".inventory_item_price"));
                }
                catch (NoSuchElementException)
                {
                    addToCartButtonForEachProduct = false;
                    break;
                }
            }

            Assert.That(addToCartButtonForEachProduct, Is.True);
        }

        [Test]
        public void ProductsPage_IncreasesCartItemCount_WhenClickingAddToCartButton()
        {
            _loginPage.LoginAs("standard_user");
            int currentItemCount;
            int startingItemCount;

            try
            {
                var cartItemCount = _productsPage.ShoppingCartItemCount.Text;
                currentItemCount = int.Parse(cartItemCount);
                startingItemCount = currentItemCount;
            }
            catch (NoSuchElementException)
            {
                startingItemCount = 0;
            }

            _productsPage.AddToCartButtons[0].Click();
            currentItemCount = int.Parse(_productsPage.ShoppingCartItemCount.Text);

            Assert.That(currentItemCount, Is.EqualTo(startingItemCount + 1));
        }

        [Test]
        public void ProductsPage_DecreasesCartItemCount_WhenRemovingItemFromCart()
        {
            _loginPage.LoginAs("standard_user");
            int currentItemCount;

            // Add first item on page to cart
            _productsPage.AddToCartButtons[0].Click();
            currentItemCount = int.Parse(_productsPage.ShoppingCartItemCount.Text);
            // Remove previously added item from cart
            _productsPage.AddToCartButtons[0].Click();

            try
            {
                currentItemCount = int.Parse(_productsPage.ShoppingCartItemCount.Text);
            }
            catch (NoSuchElementException)
            {
                currentItemCount = 0;
            }


            Assert.That(currentItemCount, Is.EqualTo(0));
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
