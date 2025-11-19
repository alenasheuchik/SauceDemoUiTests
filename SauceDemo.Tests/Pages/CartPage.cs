using OpenQA.Selenium;

namespace SauceDemo.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;
        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement CheckoutButton => _driver.FindElement(By.Id("checkout"));
        private IWebElement ContinueShoppingButton => _driver.FindElement(By.Id("continue-shopping"));
        public bool IsOpened()
        {
            return _driver.Url.Contains("cart.html");
        }
        public void Checkout()
        {
            CheckoutButton.Click();
        }
        public void ContinueShopping()
        {
            ContinueShoppingButton.Click();
        }
        public bool IsProductInCart(string productName)
        {
            return _driver.FindElements(By.XPath(
                    $"//div[@class='cart_item_label']//div[@class='inventory_item_name' and text()='{productName}']"))
                .Count > 0;
        }
        public string GetProductPrice(string productName)
        {
            var item = _driver.FindElement(By.XPath(
                $"//div[@class='cart_item'][.//div[@class='inventory_item_name' and text()='{productName}']]"));
            var priceText = item.FindElement(By.ClassName("inventory_item_price")).Text;

            return priceText.Replace("$", "").Trim();
        }
        public void RemoveProduct(string productName)
        {
            var item = _driver.FindElement(By.XPath(
                $"//div[@class='cart_item'][.//div[@class='inventory_item_name' and text()='{productName}']]"));
            item.FindElement(By.XPath(".//button[text()='Remove']")).Click();
        }
    }
}
