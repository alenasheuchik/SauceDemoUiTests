using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Allure.NUnit;

namespace SauceDemo.Tests
{
    [AllureNUnit]
    public class BaseTest
    {
        protected IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
