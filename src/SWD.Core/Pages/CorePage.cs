using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Swd.Core.WebDriver;

namespace Swd.Core.Pages
{
    public abstract class CorePage
    {
        public IWebDriver Driver { get { return SwdBrowser.Driver;  } }
        
        public CorePage()
        {
            PageFactory.InitElements(Driver, this);
        }
    }
}
