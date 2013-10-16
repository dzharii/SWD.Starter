using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Swd.Core;
using Swd.Core.Pages;
using Swd.Core.WebDriver;

using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;


namespace Demo.TestProject
{
    [TestFixture]
    public class Class1
    {

        public class YandexPage : CorePage
        {
            [FindsBy(How = How.XPath, Using = @"id(""text"")")]
            public IWebElement txtSearchBox { get; set; }

            [FindsBy(How = How.XPath, Using = @"id(""Trololo-locator"")")]
            public IWebElement txtInvalidSearchBox { get; set; }
        }
        
        [Test]
        public void FirstTest()
        {
            SwdBrowser.Driver.Navigate().GoToUrl(@"http://yandex.ru");
            var page = new YandexPage();

            page.txtSearchBox.WaitUntilVisible().SendKeys("Google");
            page.txtInvalidSearchBox.WaitUntilVisible(TimeSpan.FromDays(1)).SendKeys("Google");

        }
    }
}
