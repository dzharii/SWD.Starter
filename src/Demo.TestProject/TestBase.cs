using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Swd.Core.WebDriver;

using System.Drawing.Imaging;
using System.IO;

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
            
            
            if (isTestFailed)
            {
                try
                {
                    var myUniqueFileName = string.Format(@"screenshot_{0}.png", Guid.NewGuid());
                    var fullPath = Path.Combine(Path.GetTempPath(), myUniqueFileName);

                    var screenshot = SwdBrowser.TakeScreenshot();
                    screenshot.SaveAsFile(fullPath, ImageFormat.Png);

                    // Attach image to the log file
                    TestContext.AddResultFile(fullPath);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to take screenshot:" + e.Message);
                }
            }

            bool shouldRestartBrowser = isTestFailed;
            if (shouldRestartBrowser)
            {
                SwdBrowser.CloseDriver();
            }
        }
    }
}
