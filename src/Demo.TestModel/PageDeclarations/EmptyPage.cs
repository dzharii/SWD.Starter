using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swd.Core;
using Swd.Core.Pages;
using OpenQA.Selenium;

namespace Demo.TestModel.PageDeclarations
{
    // This is an empty page...
    // I was too lazy to implement anything... 
    public class EmptyPage: MyPageBase
    {
        public override void Invoke()
        {
            // Empty
        }

        public override bool IsDisplayed()
        {
            return true;
        }

        public override void VerifyExpectedElementsAreDisplayed()
        {
            // Empty
        }
    }
}
