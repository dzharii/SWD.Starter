using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Swd.Core.WebDriver;

namespace Demo.TestProject
{
    public abstract class TestBase
    {
        protected TestContext testContext;
        public TestContext TestContext
        {
            get { return this.testContext; }
            set { this.testContext = value; }
        }        

        
        [TestCleanup]
        public virtual void TestCleanUp()
        {
            var testResult = TestContext.CurrentTestOutcome;

            bool isTestFailed = !(testResult == UnitTestOutcome.Passed || testResult == UnitTestOutcome.Inconclusive);
            bool shouldRestartBrowser = isTestFailed;

            if (shouldRestartBrowser)
            {
                SwdBrowser.CloseDriver();
            }
        }
    }
}
