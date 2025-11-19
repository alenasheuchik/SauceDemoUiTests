using OpenQA.Selenium;
namespace SauceDemo.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private const string Url = "https://www.saucedemo.com/";

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector("h3[data-test='error']"));

        public void Open()
        {
            _driver.Navigate().GoToUrl(Url);
        }
        public void Login(string username, string password)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }
        public bool IsErrorDisplayed()
        {
            var elements = _driver.FindElements(By.CssSelector("h3[data-test='error']"));

            return elements.Count > 0 && elements[0].Displayed;
        }
    }
}
