using NUnit.Framework;

namespace ForTests
{
    [TestFixture]
    public class SmokeTests
    {
        private Steps.Steps steps = new Steps.Steps();

        [SetUp]
        public void Init()
        {
            steps.InitBrowser();
        }

        [TearDown]
        public void Cleanup()
        {
            steps.CloseBrowser();
        }

        [Test]
        public void SendQueryInSearchLineAndPushSerchButton()
        {
            string name = steps.SearchQeryStep("Телефоны Samsung");
            Assert.True(name.ToLower().Contains("телефоны samsung"));
        }

        //[Test]
        //public void OneCanLoginGithub()
        //{
        //    steps.LoginGithub(USERNAME, PASSWORD);
        //    Assert.AreEqual(USERNAME, steps.GetLoggedInUserName());
        //}

       
    }
}
