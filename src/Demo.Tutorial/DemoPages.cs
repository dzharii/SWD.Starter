using System;
using System.Linq;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

using Swd.Core.WebDriver;

namespace Demo.Tutorial
{
    public static class DemoPages
    {
        // http://stackoverflow.com/a/283917/1126595
        static public string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public static void OpenLocalHtmlFile(string pageRelativePath)
        {
            string fullPath = Path.Combine(AssemblyDirectory(), "DemoPages", pageRelativePath);
            Uri uri = new Uri(fullPath);

            string uriPath = uri.AbsoluteUri;

            SwdBrowser.Driver.Url = uriPath;
        }

        public static void Ch00_OpenHtmlFile(string relativeFilePath)
        {
            OpenLocalHtmlFile(Path.Combine("Ch_00_Introduction", relativeFilePath));
        }

    }
}
