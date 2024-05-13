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



        }
    }
}
