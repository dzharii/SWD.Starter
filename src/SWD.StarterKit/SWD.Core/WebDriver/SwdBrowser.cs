﻿using System;
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

using Swd.Core.Configuration;


namespace Swd.Core.WebDriver
{

    public static class SwdBrowser
    {
        public static event Action OnDriverStarted;
        public static event Action OnDriverClosed;

        public static Func<IWebDriver> _initializeDriver;
        public static Func<IWebDriver> InitializeDriver
        {
            get { return _initializeDriver; }
            set { _initializeDriver = value; }
        }

        private static IWebDriver _driver = null;

        static SwdBrowser()
        {
            InitializeDriver = () =>
                {
                    _driver = WebDriverRunner.Run(Config.swdBrowserType, Config.wdIsRemote, Config.wdRemoteUrl);
                    return _driver;
                };
        }

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null) Initialize();
                return _driver;
            }
        }

        public static void Initialize()
        {

            if (_driver != null)
            {
                _driver.Quit();
                if (OnDriverClosed != null) OnDriverClosed();
            }

            _driver = InitializeDriver();

            // Fire OnDriverStarted event
            if (OnDriverStarted != null)
            {
                OnDriverStarted();
            }
        }

        public static void CloseDriver()
        {

            if (_driver != null)
            {
                _driver.Dispose();

                // Fire OnDriverClosed
                if (OnDriverClosed != null)
                {
                    OnDriverClosed();

                }
            }
        }



        static readonly Finalizer finalizer = new Finalizer();
        sealed class Finalizer
        {
            ~Finalizer()
            {
                CloseDriver();
            }
        }
    }
}
