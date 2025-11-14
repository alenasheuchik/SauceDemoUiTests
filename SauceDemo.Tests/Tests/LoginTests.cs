using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using SauceDemo.Tests.Data;

namespace SauceDemo.Tests
{
    public class LoginTests : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(UsersCsvData), nameof(UsersCsvData.GetUsers))]
        public void LoginUser_Test(string username, string password, string result)
        {
            LoginPage.Open();
            LoginPage.Login(username, password);

            if (result == "success")
            {
                Assert.That(
                    ProductsPage.IsOpened(),
                    Is.True,
                    $"Юзер {username}: страница продуктов не открылась."
                );
            }
            else
            {
                Assert.That(
                    LoginPage.IsErrorDisplayed(),
                    Is.True,
                    $"Юзер {username}: ожидала ошибку, но ошибки нет."
                );
            }
        }
    }
}
