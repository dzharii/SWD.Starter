using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Swd.Core;
using Swd.Core.WebDriver;
using Swd.Core.Demo;



namespace Demo.TestProject
{
    [TestFixture]
    public class Class1
    {

        [Test]
        public void FirstTest()
        {
            SwdBrowser.Driver.Navigate().GoToUrl(@"http://yandex.ru");
        }
    }
}
