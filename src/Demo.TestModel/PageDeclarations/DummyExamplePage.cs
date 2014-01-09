using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swd.Core;
using Swd.Core.Pages;
using OpenQA.Selenium;

namespace Demo.TestModel.PageDeclarations
{
    public class DummyExamplePage : MyPageBase
    {
        public override void Invoke()
        {
            throw new NotImplementedException();
        }

        public override bool IsDisplayed()
        {
            throw new NotImplementedException();
        }

        public override void VerifyExpectedElementsAreDisplayed()
        {
            throw new NotImplementedException();
        }
    }
}
