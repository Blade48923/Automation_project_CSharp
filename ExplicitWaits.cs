using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
//using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace UnitTestProject1
{
    [TestClass]
    [TestCategory("Explicit Waits")]
    public class ExplicitWaits
    {
        private IWebDriver driver = new ChromeDriver();
        By ElementToWaitFor = By.Id("start");
        By Captcha = By.XPath("//iframe['reCAPTCHA']");

        [TestCleanup]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void ExplicitWait1()
        {
            Thread.Sleep(1000);
        }

        //Using a LINQ format of Explicit wait instead of ExpectedConditions in the SeleniumExtras.WaitHelpers
        [TestMethod]
        public void ExplicitWait2()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement element = wait.Until((d) =>
            {
                return d.FindElement(By.Id("success"));
            });
        }

        [TestMethod]
        public void Test1_FixedExplicitly()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(ElementToWaitFor)).Click();
        }

        [TestMethod]
        public void Test4_ExplicitWait_RenderedAfter()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(InvalidCastException));
            wait.Message = "Tried to find element with ID finish but the element wasn't clickable on the page after 5 seconds";
            try
            {
                var x = wait.Until(ExpectedConditions.ElementToBeClickable(ElementToWaitFor));
            }
            catch (WebDriverTimeoutException)
            {
                driver.FindElement(By.TagName("button")).Click();
                wait.Timeout = TimeSpan.FromSeconds(10);
                var x = wait.Until(ExpectedConditions.ElementToBeClickable(ElementToWaitFor));
            }
        }

        //1.)Open Page,
        //2.)Synchronize on slowest loading element,
        //3.)Proceed with actions
        [TestMethod]
        public void HowToCorrectlySynchronize()
        {
            driver.Navigate().GoToUrl("http://www.ultimateqa.com/simple-html-elements-for-automation/");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
           var slowestLoadingElement = wait.Until(ExpectedConditions.ElementIsVisible(Captcha));
            Assert.IsTrue(slowestLoadingElement.Displayed);

        }
    }

}
