using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using Fundraise.MvcExample.Tests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Fundraise.MvcExample.Tests
{
    [TestClass]
    public class AdminTests
    {
        public static IisExpressWebServer WebServer;
        public static FirefoxDriver Browser;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var app = new WebApplication(ProjectLocation.FromFolder("Fundraise.MvcExample"), 12365);
            WebServer = new IisExpressWebServer(app);
            WebServer.Start();

            Browser = new FirefoxDriver();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Browser.Quit();
            WebServer.Stop();

            // todo: reset LocalDB
        }

        [TestMethod]
        public void CreateNewAccount()
        {
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("http://localhost:12365/Account/Register");

            var emailBox = Browser.FindElementById("Email");
            emailBox.SendKeys("test@alexlindgren.com");

            var passwordBox = Browser.FindElementById("Password");
            passwordBox.SendKeys("test1234");

            var confirmPasswordBox = Browser.FindElementById("ConfirmPassword");
            confirmPasswordBox.SendKeys("test1234");

            var submitButton = Browser.FindElementById("RegisterSubmit");
            submitButton.Submit();

            try
            {
                var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
                var element = wait.Until(driver => driver.FindElement(By.Id("manage")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while waiting for 'manage': " + ex.Message);
                var screenshot = Browser.GetScreenshot();
                screenshot.SaveAsFile("error.png");
                if (Browser.PageSource.IndexOf("<code><pre>") > 0)
                {
                    Console.WriteLine(Browser.PageSource.Substring(Browser.PageSource.IndexOf("<code><pre>")));
                }
            }

            Assert.IsFalse(Browser.PageSource.Contains("An unhandled exception occurred during the execution of the current web request."), Browser.Title);
            Assert.IsTrue(Browser.Url == "http://localhost:12365/", "The browser should redirect to 'http://localhost:12365/'");
            Assert.IsTrue(Browser.PageSource.Contains("test@alexlindgren.com"), "After registering, browser should display 'Hello test@alexlindgren.com!'");
        }
    }
}
