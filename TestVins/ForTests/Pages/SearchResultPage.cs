using OpenQA.Selenium;
using System;

namespace ForTests.Pages
{
    public class SearchResultPage
    {
        private const string BASE_URL = "https://www.e-katalog.ru/";


        public IWebElement MainLabel => driver.FindElement(By.XPath("//h1[@class='t2']"));

        private readonly IWebDriver driver;

        public SearchResultPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(BASE_URL);
            Console.WriteLine("Login Page opened");
        }

        public string GetMainLabelPageText()
        {
            return this.MainLabel.Text;
        }

    }
}