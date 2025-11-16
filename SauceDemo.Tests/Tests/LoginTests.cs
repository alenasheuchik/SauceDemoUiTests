using NUnit.Framework;
using SauceDemo.Pages;
using SauceDemo.Tests.Data;

namespace SauceDemo.Tests
{
    public class LoginTests : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(UsersCsvDataReader), nameof(UsersCsvDataReader.GetUsers))]
        public void LoginUser_Test(string username, string password, string result)
        {
            var loginPage = new LoginPage(Driver);
            var productsPage = new ProductsPage(Driver);

            loginPage.Open();
            loginPage.Login(username, password);

            if (result == "success")
            {
                Assert.That(productsPage.IsOpened(), Is.True,
                    "Ожидалось открытие страницы продуктов, но она не открылась.");
            }
            else
            {
                Assert.That(loginPage.IsErrorDisplayed(), Is.True,
                    "Ожидалась ошибка, но ошибки нет.");
            }
        }
    }
}
