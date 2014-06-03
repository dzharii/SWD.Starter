using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Demo.TestModel;
using Swd.Core.Pages;
using Swd.Core.WebDriver;

namespace Demo.TestProject.Smoke
{
    [TestClass]
    public class Smoke_test_for_each_pageobject : TestBase
    {
        

        public void PageTest<PAGE>(PAGE page) where PAGE : MyPageBase, new()
        {
            // Implement Dispose inside page object in order to do cleanup
            using (page)
            {
                SwdBrowser.HandleJavaScriptErrors();
                
                page.Invoke();

                SwdBrowser.HandleJavaScriptErrors();
                
                page.VerifyExpectedElementsAreDisplayed();
                
                SwdBrowser.HandleJavaScriptErrors();
            }
        }

        
        // Add testMethods for your new pages here:
        // *PageName*_VerifyExpectedElements()
        
        [TestMethod]
        public void EmptyPage_VerifyExpectedElements()
        {
            PageTest(MyPages.EmptyPage);
        }

        [TestMethod]
        public void CreateNewAccountPage_VerifyExpectedElements()
        {
            PageTest(MyPages.CreateNewAccountPage);
        }

    }
}
