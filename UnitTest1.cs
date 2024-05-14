using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public IWebDriver driver { get; private set; }


        [TestMethod]
        public void TestMethod1()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.UltimateQA.com");
            driver.Manage().Window.Maximize();
            driver.Quit();
        }

        //private IWebDriver GetChromeDriver()
        //{
        //    var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    return new ChromeDriver(outPutDirectory);
        //}

        [TestMethod]
        [TestCategory("driver Interrogation")]
        public void DriverLevelInterrogation()
        {
            driver.Navigate().GoToUrl("http://www.ultimateqa.com/automation");
            var x = driver.CurrentWindowHandle;
            var y = driver.WindowHandles;
            x = driver.PageSource;
            x = driver.Title;
            x = driver.Url;
        }

        [TestMethod]
        [TestCategory("Element Interrogation")]
        public void ElementInterrogation()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.ultimateqa.com/automation/");
            var myElement = driver.FindElement(By.XPath("//a[normalize-space()='Big page with many elements']"));
            myElement.GetCssValue("");


        }

        [TestMethod]
        [TestCategory("Element Interrogation")]
        public void ElementInterrogation2()
        {
            var driver = new ChromeDriver();
            driver.Url = "http://www.ultimateqa.com/simple-html-elements-for-automation/";
            driver.Manage().Window.Maximize();
            //Find button by ID
            var button = driver.FindElement(By.Id("idExample"));
            //GetAttribut("type") and assert that it equals the right value
            Assert.AreEqual("", button.GetAttribute("type"));
            //GetCssValue("letter-spacing") and assert that it equals the correct value
            Assert.AreEqual("normal", button.GetCssValue("letter-spacing"));
            //Assert that it's displayed
            Assert.IsTrue(button.Displayed);
            //Assert that it's Enabled
            Assert.IsTrue(button.Enabled);
            //Assert that it's NOT selected
            Assert.IsFalse(button.Selected);
            //Assert that Text is correct
            Assert.AreEqual(button.Text, "Click this button using \"ID\"");
            //Assert that the TagName is correct
            Assert.AreEqual("a", button.TagName);
            //Assert that the size height is 21
            Assert.AreEqual(63, button.Size.Height);
            //Assert that the location is x=190, y = 330
            Assert.AreEqual(661, button.Location.X);
            Assert.AreEqual(486, button.Location.Y);

            driver.Quit();
        }
    }
}
