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
        public const string email = "test28@alexlindgren.com";

        [ClassInitialize]
        [TestProperty("TestKind", "Browser")]
        public static void Init(TestContext context)
        {
            var app = new WebApplication(ProjectLocation.FromFolder("Fundraise.MvcExample"), 12365);
            WebServer = new IisExpressWebServer(app);
            WebServer.Start();
            Browser = new FirefoxDriver();

            // Create a test user
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("http://localhost:12365/Account/Register");

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
                var element = wait.Until(ExpectedConditions.UrlToBe("http://localhost:59702/"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while waiting for 'Edit' link: " + ex.Message);
                Console.WriteLine("Final URL was " + Browser.Url);
                var screenshot2 = Browser.GetScreenshot();
                screenshot2.SaveAsFile("login-error.png");
                if (Browser.PageSource.IndexOf("<code><pre>") > 0)
                {
                    Console.WriteLine(Browser.PageSource.Substring(Browser.PageSource.IndexOf("<code><pre>")));
                }
                else
                {
                    Console.WriteLine("Final page title was: " + Browser.Title);
                }
            }
            Console.WriteLine("complete ClassInitialize");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Browser.Quit();
            WebServer.Stop();
        }

        [TestMethod]
        public void CheckLoggedIn()
        {
            Browser.Navigate().GoToUrl("http://localhost:12365/");

            Assert.IsFalse(Browser.PageSource.Contains("Register"));
            Assert.IsFalse(Browser.PageSource.Contains("Log in"));
            Assert.IsTrue(Browser.PageSource.Contains(email), "Logging in, the browser should display 'Hello " + email + "!'");
        }


        [TestMethod]
        public void Login()
        {
            Browser.Navigate().GoToUrl("http://localhost:12365/Account/Login");

            var emailBox = Browser.FindElementById("Email");
            emailBox.SendKeys(email);

            var passwordBox = Browser.FindElementById("Password");
            passwordBox.SendKeys("test1234");

            var submitButton = Browser.FindElementById("LoginSubmit");
            submitButton.Submit();

            var screenshot = Browser.GetScreenshot();
            screenshot.SaveAsFile("error.png");

            Assert.IsFalse(Browser.PageSource.Contains("An unhandled exception occurred during the execution of the current web request."), Browser.Title);
            Assert.IsTrue(Browser.Url == "http://localhost:12365/", "The browser should redirect to 'http://localhost:12365/'");
            Assert.IsTrue(Browser.PageSource.Contains(email), "After logging in, the browser should display 'Hello " + email + "!'");
        }

        //[TestMethod]
        //public void CreateNewCampaign()
        //{
        //    Browser.Navigate().GoToUrl("http://localhost:12365/Admin/CampaignCreate");

        //    var nameBox = Browser.FindElementById("Name");
        //    nameBox.SendKeys("Test Campaign");

        //    var descBox = Browser.FindElementById("Description");
        //    descBox.SendKeys("This is a test.");

        //    var saveButton = Browser.FindElementById("campaign-create");
        //    //Console.WriteLine("save button value: " + saveButton.GetAttribute("value").ToString());
        //    saveButton.Submit();

        //    var screenshot2 = Browser.GetScreenshot();
        //    screenshot2.SaveAsFile("create-campaign-response.png");

        //    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(30));
        //    var element = wait.Until(ExpectedConditions.UrlContains("/Admin/CampaignDetail/"));

        //    Console.WriteLine("Browser.Url: " + Browser.Url);

        //    Assert.IsTrue(Browser.Url.Contains("/Admin/CampaignDetail/"), "The browser should redirect to 'http://localhost:12365/Admin/CampaignDetail/[GUID]'");
        //}

        //[TestMethod]
        //public void CreateNewFundraiser()
        //{
        //    Browser.Manage().Window.Maximize();
        //    Browser.Navigate().GoToUrl("http://localhost:12365/Admin");

        //    var createLink = Browser.FindElementByClassName("fundraiser-create");
        //    createLink.Click();

        //    try
        //    {
        //        var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(8));
        //        var element = wait.Until(driver => driver.FindElement(By.Id("IsActive")));
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception while waiting for 'IsActive' checkbox: " + ex.Message);
        //        var screenshot = Browser.GetScreenshot();
        //        screenshot.SaveAsFile("error.png");
        //        if (Browser.PageSource.IndexOf("<code><pre>") > 0)
        //        {
        //            Console.WriteLine(Browser.PageSource.Substring(Browser.PageSource.IndexOf("<code><pre>")));
        //        }
        //    }
        //    Assert.IsTrue(Browser.Url.Contains("/Admin/FundraiserCreate?campaignId="), "The browser should redirect to 'http://localhost:12365/Admin/FundraiserCreate?campaignId=[GUID]'");

        //    var nameBox = Browser.FindElementById("Name");
        //    nameBox.SendKeys("Test Fundraiser");

        //    var descBox = Browser.FindElementById("Description");
        //    nameBox.SendKeys("This is a test.");

        //    var activeBox = Browser.FindElementById("IsActive");
        //    activeBox.Click();

        //    var saveButton = Browser.FindElementsByCssSelector("input.btn-success")[0];
        //    saveButton.Submit();


        //    try
        //    {
        //        var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(8));
        //        var element = wait.Until(driver => driver.FindElement(By.LinkText("Test Fundraiser")));
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception while waiting for 'Test Fundraiser' link: " + ex.Message);
        //        var screenshot = Browser.GetScreenshot();
        //        screenshot.SaveAsFile("error.png");
        //        if (Browser.PageSource.IndexOf("<code><pre>") > 0)
        //        {
        //            Console.WriteLine(Browser.PageSource.Substring(Browser.PageSource.IndexOf("<code><pre>")));
        //        }
        //    }
        //    Assert.IsTrue(Browser.Url == "http://localhost:12365/Admin", "The browser should redirect to 'http://localhost:12365/Admin'");
        //    Assert.IsTrue(Browser.PageSource.Contains("Test Fundraiser"), "After registering, browser should display a link with text 'Test Fundraiser'");
        //}
    }
    }
