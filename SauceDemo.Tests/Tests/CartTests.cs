using NUnit.Framework;
using SauceDemo.Pages;
using SauceDemo.Tests.Data;

namespace SauceDemo.Tests
{
    public class CartTests : BaseTest
    {
        private LoginPage _loginPage;
        private ProductsPage _productsPage;
        private CartPage _cartPage;

        [SetUp]
        public void SetUpPages()
        {
            _loginPage = new LoginPage(Driver);
            _productsPage = new ProductsPage(Driver);
            _cartPage = new CartPage(Driver);
        }

        [Test]
        [TestCaseSource(typeof(ProductsCsvDataReader), nameof(ProductsCsvDataReader.GetProducts))]
        public void AddProductToCart_Test(string productName, string expectedPrice)
        {
            _loginPage.Open();
            _loginPage.Login("standard_user", "secret_sauce");

            Assert.That(_productsPage.IsOpened(), Is.True,
                "Ожидалось, что откроется страница с продуктами.");

            _productsPage.AddProductToCart(productName);

            Assert.That(_productsPage.GetCartBadgeCount(), Is.EqualTo("1"),
                "Ожидался один товар в корзине.");

            _productsPage.OpenCart();

            Assert.Multiple(() =>
            {
                Assert.That(_cartPage.IsOpened(), Is.True,
                    "Ожидалось открытие страницы корзины.");

                Assert.That(_cartPage.IsProductInCart(productName), Is.True,
                    $"Товар '{productName}' не найден в корзине.");

                Assert.That(_cartPage.GetProductPrice(productName), Is.EqualTo(expectedPrice),
                    $"Неверная цена для товара '{productName}'.");
            });

            _cartPage.RemoveProduct(productName);
        }
    }
}
