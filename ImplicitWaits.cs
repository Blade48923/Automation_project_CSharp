using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UnitTestProject1
{
    [TestClass]
    public class ImplicitWaits
    {

        //Driver Setup for Test runs
        private IWebDriver _driver = new ChromeDriver();

        //Test cleanup method for after test cases are ran
        [TestCleanup]
        public void Teardown()
        {
            _driver.Close();
            _driver.Quit();
        }

        [TestMethod]
        [ExpectedException (typeof(NoSuchElementException))]
        public void Test1_ImplicitWaitExample()
        {
            _driver.Navigate().GoToUrl("http://www.ultimateqa.com");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.IsTrue(_driver.FindElement(By.Id("success")).Displayed);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementNotInteractableException))]
        public void Test2_ImplicitWait_HiddenElement()
        {
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
            SetImplicitWaitAndClick();
        }

        [TestMethod]
        [ExpectedException(typeof(NoSuchElementException))]
        public void Test3_ImplicitWait_RenderedAfter()
        {
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");
            SetImplicitWaitAndClick();
        }

        private void SetImplicitWaitAndClick()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.FindElement(By.Id("finish")).Click();
        }
    }
}
