using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.Data;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        public IWebDriver Driver { get; private set; }
        [TestInitialize]
        public void SetupBeforeEveryTestMethod()
        {
            
            Driver = new ChromeDriver();
            //driver.Manage().Window.Maximize();
        }
        private void HighlightElementUsingJavaScript(By locationStrategy, int duration = 2)
        {
            var element = Driver.FindElement(locationStrategy);
            var originalStyle = element.GetAttribute("style");
            IJavaScriptExecutor JavaScriptExecutor = Driver as IJavaScriptExecutor;
            JavaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                element,
                "style",
                "border: 7px solid yellow; border-style: dashed;");

            if (duration <= 0) return;
            Thread.Sleep(TimeSpan.FromSeconds(duration));
            JavaScriptExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                element,
                "style",
                originalStyle);
        }
        [TestMethod]
        public void TestMethod1()
        {
            /*
             *-Using only XPath!!
             -When debugging and testing, make sure that you scroll the element into view, Selenium
             will not scroll for you. Not yet...
             */
            //var driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("http://www.ultimateqa.com/simple-html-elements-for-automation/");
            //click any radio button, hint:  FindElement().Click();
            Driver.FindElement(By.XPath("//input[@name='gender'][@value='male']")).Click();
            //select one checkbox
            Driver.FindElement(By.XPath("//*[@name='vehicle'][@value='Car']")).Click();
            //select Audi from the dropdown
            Driver.FindElement(By.XPath("//*[@class='et_pb_blurb_description']/select")).Click();
            Thread.Sleep(400);
            Driver.FindElement(By.XPath("//option[@value='audi']")).Click();
            //open Tab2 and assert that it is opened. Hint, use .Text property when you find the element
            Driver.FindElement(By.XPath("//*[@class='et_pb_tab_1']")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("Tab 2 content",Driver.FindElement(By.XPath("//div[contains(text(),'Tab 2 content')]")).Text);

            //in the HTML Table with id, highlight one of the salary cells
            HighlightElementUsingJavaScript(By.XPath("//td[contains(text(),'$150,000+')]"));

            //Highlight the center section called "Highlight me", but you can only
            //highlight the highest level div for that element. The top parent div.
            //Hint, this is the class - 
            //et_pb_column et_pb_column_1_3  et_pb_column_10 et_pb_css_mix_blend_mode_passthrough
            HighlightElementUsingJavaScript(By.XPath("//*[@class='et_pb_column et_pb_column_1_3 et_pb_column_9  et_pb_css_mix_blend_mode_passthrough']"));


        }


        [TestMethod]
        public void TestMethod2()
        {

            //Go here and assert for title - "https://www.ultimateqa.com"
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Assert.AreEqual("Homepage - Ultimate QA", Driver.Title);
            //Go here and assert for title - "https://www.ultimateqa.com/automation"
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            Assert.AreEqual("Automation Practice - Ultimate QA", Driver.Title);
            //Click link with href - /complicated-page
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("//a[normalize-space()='Big page with many elements']")).Click();
            Assert.AreEqual("Complicated Page - Ultimate QA", Driver.Title);
            //assert page title 'Complicated Page - Ultimate QA'
            //Go back
            Driver.Navigate().Back();
            //assert page title equals - 'Automation Practice - Ultimate QA'
            Assert.AreEqual("Automation Practice - Ultimate QA", Driver.Title);
        }

        [TestMethod]
        [TestCategory("Manipulation")]
        public void Manipulation()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");
            //find the name field
            var nameField = Driver.FindElement(By.Id("et_pb_contact_name_0"));
            //clear the field
            nameField.Clear();
            //Type into the field
            nameField.SendKeys("Channing Hockenberry");
            //find the text field
            var textField = Driver.FindElement(By.XPath("//textarea[@id='et_pb_contact_message_0']"));
            //clear the field
            textField.Clear();
            //type into the field
            textField.SendKeys("This is the Text Field");
            //submit
            var submitButton = Driver.FindElement(By.XPath("//*[@id='et_pb_contact_form_0']/div[2]/form/div/button"));
            submitButton.Submit();

        }

        [TestMethod]
        [TestCategory("Manipulation Quiz")]
        public void Manipulation_Quiz()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");
            var nameField = Driver.FindElement(By.Id("et_pb_contact_name_1"));
            nameField.Clear();
            nameField.SendKeys("Jerry");
            var messageField = Driver.FindElement(By.Id("et_pb_contact_message_1"));
            messageField.Clear();
            messageField.SendKeys("This is Text by automation");

            //Captcha solution 
            var captcha = Driver.FindElement(By.XPath("//span[@class='et_pb_contact_captcha_question']"));
            var table = new DataTable();
            var captchaAnswer = (int)table.Compute(captcha.Text, "");
            var captchaTextbox = Driver.FindElement(By.XPath("//input[@name='et_pb_contact_captcha_1']"));
            captchaTextbox.SendKeys(captchaAnswer.ToString());
            
            //Submit button
            var submitButton = Driver.FindElement(By.XPath("//div[@id='et_pb_contact_form_1']//button[@name='et_builder_submit_button'][normalize-space()='Submit']"));
            submitButton.Submit();

            Thread.Sleep(500);
            var afterSubmit = Driver.FindElements(By.ClassName("et-pb-contact-message"))[1].FindElement(By.TagName("p)"));
            Assert.IsTrue(afterSubmit.Text.Equals("Thanks for contacting us"));
        }


        [TestCleanup]
        public void TestCleanup()
        {
            Driver.Quit();
        }
    }
}
