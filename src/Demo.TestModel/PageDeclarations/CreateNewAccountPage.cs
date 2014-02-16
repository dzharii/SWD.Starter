
#region Usings - System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
#region Usings - SWD
using Swd.Core;
using Swd.Core.Pages;
using Swd.Core.WebDriver;
#endregion
#region Usings - WebDriver
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
#endregion
namespace Demo.TestModel.PageDeclarations
{
    public class CreateNewAccountPage : MyPageBase
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = @"id(""wpName2"")")]
        protected IWebElement txtUserName { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""wpPassword2"")")]
        protected IWebElement txtPassword { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""wpRetype"")")]
        protected IWebElement txtConfirmPassword { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""wpEmail"")")]
        protected IWebElement txtEmailAddress { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""wpCaptchaWord"")")]
        protected IWebElement txtCaptcha { get; set; }


        [FindsBy(How = How.XPath, Using = @"id(""wpCreateaccount"")")]
        protected IWebElement btnCreateYourAccount { get; set; }

        #endregion

        #region Invoke and Exists
        public override void Invoke()
        {
            Driver.Url = @"https://en.wikipedia.org/w/index.php?title=Special:UserLogin&returnto=Main+Page&type=signup";
        }

        public override bool IsDisplayed()
        {
            return txtCaptcha.Displayed;
        }
        #endregion

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("txtUserName", txtUserName);
            VerifyElementVisible("txtPassword", txtPassword);
            VerifyElementVisible("txtConfirmPassword", txtConfirmPassword);
            VerifyElementVisible("txtEmailAddress", txtEmailAddress);
            VerifyElementVisible("txtCaptcha", txtCaptcha);
            VerifyElementVisible("btnCreateYourAccount", btnCreateYourAccount);
        }
    }
}