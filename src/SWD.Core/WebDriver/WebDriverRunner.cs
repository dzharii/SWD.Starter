using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.PhantomJS;

namespace Swd.Core.WebDriver
{
    public class WebDriverRunner
    {
        public const string browser_Firefox = "Firefox";
        public const string browser_Chrome = "Chrome";
        public const string browser_InternetExplorer = "InternetExplorer";
        public const string browser_PhantomJS = "PhantomJS";
        public const string browser_HtmlUnit = "HtmlUnit";
        public const string browser_HtmlUnitWithJavaScript = "HtmlUnitWithJavaScript";
        public const string browser_Opera = "Opera";
        public const string browser_Safari = "Safari";
        public const string browser_IPhone = "IPhone";
        public const string browser_IPad = "IPad";
        public const string browser_Android = "Android";


        public static IWebDriver Run(string browserName, bool isRemote, string remoteUrl)
        {
            IWebDriver driver = null;
            if (isRemote)
            {
                driver = ConnetctToRemoteWebDriver(browserName, remoteUrl);
            }
            else
            {
                driver = StartEmbededWebDriver(browserName);
            }
            return driver;
        }

        private static IWebDriver ConnetctToRemoteWebDriver(string browserName, string remoteUrl)
        {
            DesiredCapabilities caps = null;
            Uri hubUri = new Uri(remoteUrl);

            switch (browserName)
            {

                case browser_Firefox:
                    caps = DesiredCapabilities.Firefox();
                    break;
                case browser_Chrome:
                    caps = DesiredCapabilities.Chrome();
                    break;
                case browser_InternetExplorer:
                    caps = DesiredCapabilities.InternetExplorer();
                    break;
                case browser_PhantomJS:
                    caps = DesiredCapabilities.PhantomJS();
                    break;
                case browser_HtmlUnit:
                    caps = DesiredCapabilities.HtmlUnit();
                    break;
                case browser_HtmlUnitWithJavaScript:
                    caps = DesiredCapabilities.HtmlUnitWithJavaScript();
                    break;
                case browser_Opera:
                    caps = DesiredCapabilities.Opera();
                    break;
                case browser_Safari:
                    caps = DesiredCapabilities.Safari();
                    break;
                case browser_IPhone:
                    caps = DesiredCapabilities.IPhone();
                    break;
                case browser_IPad:
                    caps = DesiredCapabilities.IPad();
                    break;
                case browser_Android:
                    caps = DesiredCapabilities.Android();
                    break;
                default:
                    throw new ArgumentException(String.Format(@"<{0}> was not recognized as supported browser. This parameter is case sensitive", browserName),
                                                "WebDriverOptions.BrowserName");
            }
            RemoteWebDriver newDriver = new RemoteWebDriver(hubUri, caps);
            return newDriver;
        }

        private static IWebDriver StartEmbededWebDriver(string browserName)
        {
            switch (browserName)
            {

                case browser_Firefox:
                    return new FirefoxDriver();
                case browser_Chrome:
                    return new ChromeDriver();
                case browser_InternetExplorer:
                    return new InternetExplorerDriver();
                case browser_PhantomJS:
                    return new PhantomJSDriver();
                case browser_Safari:
                    return new SafariDriver();
                default:
                    throw new ArgumentException(String.Format(@"<{0}> was not recognized as supported browser. This parameter is case sensitive", browserName),
                                                "WebDriverOptions.BrowserName");
            }
        }


    }
}
