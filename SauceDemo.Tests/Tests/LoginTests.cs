using NUnit.Framework;
using SauceDemo.Pages;
using SauceDemo.Tests.Data;

namespace SauceDemo.Tests
{
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage;
        private ProductsPage _productsPage;

        [SetUp]
        public void SetUpPages()
        {
            _loginPage = new LoginPage(Driver);
            _productsPage = new ProductsPage(Driver);
        }

        [Test]
        [TestCaseSource(typeof(UsersCsvDataReader), nameof(UsersCsvDataReader.GetUsers))]
        public void LoginUser_Test(string username, string password, string result)
        {
            _loginPage.Open();
            _loginPage.Login(username, password);

            if (result == "success")
            {
                Assert.That(_productsPage.IsOpened(), Is.True,
                    "Ожидалось открытие страницы продуктов, но она не открылась.");
            }
            else
            {
                Assert.That(_loginPage.IsErrorDisplayed(), Is.True,
                    "Ожидалась ошибка, но ошибки нет.");
            }
        }
    }
}
