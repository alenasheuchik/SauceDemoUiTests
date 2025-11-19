using OpenQA.Selenium;
namespace SauceDemo.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement CartBadge => _driver.FindElement(By.ClassName("shopping_cart_badge"));
        private IWebElement CartLink => _driver.FindElement(By.Id("shopping_cart_container"));

        public bool IsOpened()
        {
            return _driver.Url.Contains("inventory.html");
        }

        public void AddProductToCart(string productName)
        {
            var nameElement = _driver.FindElement(By.XPath(
                $"//div[@class='inventory_item']//div[contains(@class,'inventory_item_name') and contains(text(),'{productName}')]"));

            var itemContainer = nameElement.FindElement(By.XPath("./ancestor::div[@class='inventory_item']"));
            itemContainer.FindElement(By.XPath(".//button")).Click();
        }

        public string GetCartBadgeCount()
        {
            return CartBadge.Text;
        }

        public void OpenCart()
        {
            CartLink.Click();
        }

        public bool IsProductVisibleOnProductsPage(string productName)
        {
            return _driver.FindElements(By.XPath(
                    $"//div[@class='inventory_item']//div[contains(@class,'inventory_item_name') and contains(text(),'{productName}')]"))
                .Count > 0;
        }
    }
}
