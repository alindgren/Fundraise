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

            string email = "test14@alexlindgren.com";
            var emailBox = Browser.FindElementById("Email");
            emailBox.SendKeys(email);

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
            Assert.IsTrue(Browser.PageSource.Contains(email), "After registering, browser should display 'Hello "+ email + "!'");

            Browser.Navigate().GoToUrl("http://localhost:12365/Admin/CampaignCreate");

            var screenshot1 = Browser.GetScreenshot();
            screenshot1.SaveAsFile("create-campaign.png");

            var nameBox = Browser.FindElementById("Name");
            nameBox.SendKeys("Test Campaign");

            var descBox = Browser.FindElementById("Description");
            descBox.SendKeys("This is a test.");

            var saveButton = Browser.FindElementsByCssSelector("input.btn-success")[0];
            saveButton.Submit();

            try
            {
                var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
                var element = wait.Until(driver => driver.FindElement(By.LinkText("Edit")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while waiting for 'Edit' link: " + ex.Message);
                var screenshot = Browser.GetScreenshot();
                screenshot.SaveAsFile("create-campaign-error.png");
                if (Browser.PageSource.IndexOf("<code><pre>") > 0)
                {
                    Console.WriteLine(Browser.PageSource.Substring(Browser.PageSource.IndexOf("<code><pre>")));
                }
            }
            Assert.IsTrue(Browser.Url.Contains("/Admin/CampaignDetail/"), "The browser should redirect to 'http://localhost:12365/Admin/CampaignDetail/[GUID]'");
        }

        [TestMethod]
        public void CreateNewFundraiser()
        {
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("http://localhost:12365/Admin");

            var createLink = Browser.FindElementByClassName("fundraiser-create");
            createLink.Click();

            try
            {
                var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(8));
                var element = wait.Until(driver => driver.FindElement(By.Id("IsActive")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while waiting for 'IsActive' checkbox: " + ex.Message);
                var screenshot = Browser.GetScreenshot();
                screenshot.SaveAsFile("error.png");
                if (Browser.PageSource.IndexOf("<code><pre>") > 0)
                {
                    Console.WriteLine(Browser.PageSource.Substring(Browser.PageSource.IndexOf("<code><pre>")));
                }
            }
            Assert.IsTrue(Browser.Url.Contains("/Admin/FundraiserCreate?campaignId="), "The browser should redirect to 'http://localhost:12365/Admin/FundraiserCreate?campaignId=[GUID]'");

            var nameBox = Browser.FindElementById("Name");
            nameBox.SendKeys("Test Fundraiser");

            var descBox = Browser.FindElementById("Description");
            nameBox.SendKeys("This is a test.");

            var activeBox = Browser.FindElementById("IsActive");
            activeBox.Click();

            var saveButton = Browser.FindElementsByCssSelector("input.btn-success")[0];
            saveButton.Submit();


            try
            {
                var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(8));
                var element = wait.Until(driver => driver.FindElement(By.LinkText("Test Fundraiser")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while waiting for 'Test Fundraiser' link: " + ex.Message);
                var screenshot = Browser.GetScreenshot();
                screenshot.SaveAsFile("error.png");
                if (Browser.PageSource.IndexOf("<code><pre>") > 0)
                {
                    Console.WriteLine(Browser.PageSource.Substring(Browser.PageSource.IndexOf("<code><pre>")));
                }
            }
            Assert.IsTrue(Browser.Url == "http://localhost:12365/Admin", "The browser should redirect to 'http://localhost:12365/Admin'");
            Assert.IsTrue(Browser.PageSource.Contains("Test Fundraiser"), "After registering, browser should display a link with text 'Test Fundraiser'");
        }
    }
}
