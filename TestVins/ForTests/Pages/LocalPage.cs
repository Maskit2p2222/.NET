using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForTests.Pages
{
    class LocalPage
    {
        private const string BASE_URL = "https://www.e-katalog.ru/";

        private IWebElement InputSearchLine => driver.FindElement(By.Id("ek-search"));

        private IWebElement SearchButton => driver.FindElement(By.Name("search_but_"));

        private readonly IWebDriver driver;

        public LocalPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public LocalPage OpenPage()
        {
            driver.Navigate().GoToUrl(BASE_URL);
            Console.WriteLine("Login Page opened");
            return this;
        }

        public LocalPage SendSearchQuery(string searchQuery)
        {
            InputSearchLine.SendKeys(searchQuery);
            return this;
        }

        public SearchResultPage ClickOnSearchButton()
        {
            SearchButton.Click();
            return new SearchResultPage(driver);
        }

    }
}
