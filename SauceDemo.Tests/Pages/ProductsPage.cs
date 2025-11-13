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

        // Проверяем, что открылась страница с товарами
        public bool IsOpened()
        {
            return _driver.Url.Contains("inventory.html");
        }

        // Добавляем товар в корзину по имени
        public void AddProductToCart(string productName)
        {
            // Находим элемент с названием товара (допускаем пробелы/переносы строк)
            var nameElement = _driver.FindElement(By.XPath(
                $"//div[@class='inventory_item']//div[contains(@class,'inventory_item_name') and contains(text(),'{productName}')]"));

            // Поднимаемся к карточке товара
            var itemContainer = nameElement.FindElement(By.XPath("./ancestor::div[@class='inventory_item']"));

            // Находим кнопку и кликаем её
            itemContainer.FindElement(By.XPath(".//button")).Click();
        }

        // Получаем цифру на бейдже корзины
        public string GetCartBadgeCount()
        {
            return _driver.FindElement(By.ClassName("shopping_cart_badge")).Text;
        }

        // Переходим в корзину
        public void OpenCart()
        {
            _driver.FindElement(By.Id("shopping_cart_container")).Click();
        }

        // Проверяем, что товар виден на странице продуктов
        public bool IsProductVisibleOnProductsPage(string productName)
        {
            return _driver.FindElements(By.XPath(
                $"//div[@class='inventory_item']//div[contains(@class,'inventory_item_name') and contains(text(),'{productName}')]"))
                .Count > 0;
        }
    }
}
