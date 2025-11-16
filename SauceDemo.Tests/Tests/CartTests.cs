using NUnit.Framework;
using SauceDemo.Pages;
using SauceDemo.Tests.Data;

namespace SauceDemo.Tests
{
    public class CartTests : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(ProductsCsvDataReader), nameof(ProductsCsvDataReader.GetProducts))]
        public void AddProductToCart_Test(string productName, string expectedPrice)
        {
            var loginPage = new LoginPage(Driver);
            var productsPage = new ProductsPage(Driver);
            var cartPage = new CartPage(Driver);

            loginPage.Open();
            loginPage.Login("standard_user", "secret_sauce");

            Assert.That(productsPage.IsOpened(), Is.True,
                "Ожидалось, что откроется страница с продуктами.");

            productsPage.AddProductToCart(productName);

            Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo("1"),
                "Ожидался один товар в корзине.");

            productsPage.OpenCart();

            Assert.Multiple(() =>
            {
                Assert.That(cartPage.IsOpened(), Is.True,
                    "Ожидалось открытие страницы корзины.");

                Assert.That(cartPage.IsProductInCart(productName), Is.True,
                    $"Товар '{productName}' не найден в корзине.");

                Assert.That(cartPage.GetProductPrice(productName), Is.EqualTo(expectedPrice),
                    $"Неверная цена для товара '{productName}'.");
            });

            cartPage.RemoveProduct(productName);
        }
    }
}
