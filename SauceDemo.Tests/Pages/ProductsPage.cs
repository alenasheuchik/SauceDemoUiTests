using System.Collections.Generic;
using OpenQA.Selenium;

namespace SauceDemo.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;

        private const string InventoryPageUrlPart = "inventory.html";

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement CartBadge =>
            _driver.FindElement(By.ClassName("shopping_cart_badge"));

        private IWebElement CartLink =>
            _driver.FindElement(By.Id("shopping_cart_container"));

        private readonly By inventoryItemLocator =
            By.ClassName("inventory_item");

        private readonly By itemNameLocator =
            By.ClassName("inventory_item_name");

        private readonly By addToCartButtonLocator =
            By.TagName("button");

        private IReadOnlyCollection<IWebElement> InventoryItems =>
            _driver.FindElements(inventoryItemLocator);

        private IWebElement GetInventoryItemByName(string productName)
        {
            foreach (var item in InventoryItems)
            {
                var nameElement = item.FindElement(itemNameLocator);

                if (nameElement.Text == productName)
                {
                    return item;
                }
            }

            return null;
        }
        public bool IsOpened()
        {
            return _driver.Url.Contains(InventoryPageUrlPart);
        }
        public void AddProductToCart(string productName)
        {
            var item = GetInventoryItemByName(productName);

            if (item == null)
            {
                return;
            }

            var button = item.FindElement(addToCartButtonLocator);
            button.Click();
        }
        public string GetCartBadgeCount()
        {
            try
            {
                return CartBadge.Text;
            }
            catch (NoSuchElementException)
            {
                return "0";
            }
        }
        public void OpenCart()
        {
            CartLink.Click();
        }
        public bool IsProductVisibleOnProductsPage(string productName)
        {
            var item = GetInventoryItemByName(productName);

            return item != null;
        }
    }
}
