using OpenQA.Selenium;

namespace ForTests.Steps
{
    public class Steps
    {
        IWebDriver driver;

        public void InitBrowser()
        {
            driver = Driver.DriverInstance.GetInstance();
        }

        public void CloseBrowser()
        {
            Driver.DriverInstance.CloseBrowser();
        }

        public string SearchQeryStep(string searchQuery)
        {
            Pages.LocalPage local = new Pages.LocalPage(driver);
            return local
                .OpenPage()
                .SendSearchQuery(searchQuery)
                .ClickOnSearchButton()
                .GetMainLabelPageText();

        }


    }
}
