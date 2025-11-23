using System.Collections.Generic;
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
        private IWebElement CheckoutButton =>
            _driver.FindElement(By.Id("checkout"));
        private IWebElement ContinueShoppingButton =>
            _driver.FindElement(By.Id("continue-shopping"));
        private IReadOnlyCollection<IWebElement> CartItems =>
            _driver.FindElements(By.CssSelector(".cart_item"));
        private IWebElement GetCartItemByName(string productName)
        {
            foreach (var item in CartItems)
            {
                var nameElement = item.FindElement(By.ClassName("inventory_item_name"));

                if (nameElement.Text == productName)
                {
                    return item;
                }
            }

            return null;
        }
        private IWebElement GetPriceElement(IWebElement cartItem) =>
            cartItem.FindElement(By.ClassName("inventory_item_price"));
        private IWebElement GetRemoveButton(IWebElement cartItem) =>
            cartItem.FindElement(By.XPath(".//button[text()='Remove']"));
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
            var item = GetCartItemByName(productName);

            return item != null;
        }
        public string GetProductPrice(string productName)
        {
            var item = GetCartItemByName(productName);

            if (item == null)
            {
                return string.Empty;
            }

            var priceText = GetPriceElement(item).Text;

            return priceText.Replace("$", "").Trim();
        }
        public void RemoveProduct(string productName)
        {
            var item = GetCartItemByName(productName);

            if (item == null)
            {
                return;
            }

            GetRemoveButton(item).Click();
        }
    }
}
