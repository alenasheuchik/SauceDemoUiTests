using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using SauceDemo.Tests.Data;

namespace SauceDemo.Tests
{
    public class CartTests : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(ProductsCsvData), nameof(ProductsCsvData.GetProducts))]
        public void AddProductToCart_Test(string productName, string expectedPrice)
        {
            // 1 - Логин
            LoginPage.Open();
            LoginPage.Login("standard_user", "secret_sauce");

            // 2 - Проверка, что открыта страница с продуктами
            Assert.That(
                ProductsPage.IsOpened(),
                Is.True,
                "Страница продуктов не открылась после логина"
            );

            // 3 - Добавляем продукт
            ProductsPage.AddProductToCart(productName);

            // 4 - Проверяем цифру на корзине
            Assert.That(
                ProductsPage.GetCartBadgeCount(),
                Is.EqualTo("1"),
                "Ожидали 1 товар в корзине"
            );

            // 5 - Переходим в корзину
            ProductsPage.OpenCart();

            // 6,7,8 - софт-ассерты
            Assert.Multiple(() =>
            {
                // 6 - открыта страница корзины
                Assert.That(
                    CartPage.IsOpened(),
                    Is.True,
                    "Страница корзины не открылась"
                );

                // 7 - выбранный продукт в корзине
                Assert.That(
                    CartPage.IsProductInCart(productName),
                    Is.True,
                    $"Товар '{productName}' не найден в корзине"
                );

                // 8 - цена продукта
                Assert.That(
                    CartPage.GetProductPrice(productName),
                    Is.EqualTo(expectedPrice),
                    $"Неверная цена для товара '{productName}'"
                );
            });

            // 9 - Убираем продукт
            CartPage.RemoveProduct(productName);
        }
    }
}
