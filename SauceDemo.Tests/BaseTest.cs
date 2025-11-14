using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Pages;

namespace SauceDemo.Tests
{
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected LoginPage LoginPage;
        protected ProductsPage ProductsPage;
        protected CartPage CartPage;

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            LoginPage = new LoginPage(Driver);
            ProductsPage = new ProductsPage(Driver);
            CartPage = new CartPage(Driver);
        }
        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
