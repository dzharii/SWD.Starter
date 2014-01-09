using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Swd.Core.WebDriver;

namespace Swd.Core.Pages
{
    public abstract class SelfTestingCorePage : CorePage, Invokable, ICheckExpectedWebElements
    {
        public abstract void Invoke();

        public abstract bool IsDisplayed();

        public abstract void VerifyExpectedElementsAreDisplayed();
    }
}
