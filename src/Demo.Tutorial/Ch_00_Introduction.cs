using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Swd.Core.WebDriver;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

//using op

namespace Demo.Tutorial
{
    [TestClass]
    public class Ch_00_Introduction
    {

        /*  Dear Reader! 
            ==============
            
            What a great moment! I’ve decided to start crafting Selenium WebDriver tests. 
            So, let’s start our journey.
        */


        /*  First step: Run the WebDriver
            -----------------------------
            
            Let’s start from a very simple, but *very* important action. Let’s open a page in the Web Browser. 
            Please, make sure you have installed Firefox on your computer. Otherwise – the code will fail. 
            So, please find the test with name S01_First_Step_Run_WebDriver_with_Firefox below 
            and RIGHT-click on the line: `SwdBrowser.Driver.Url ...`.
            
            In the context menu, select `Run Tests`.
         
            ...
         
            Have you seen the browser appeared? O_O
        */
        [TestMethod]
        public void S01_First_Step_Run_WebDriver_with_Firefox()
        {
            
            SwdBrowser.Driver.Url = "http://swd-tools.com";

        }

        /* 
            I hope the test code went  smoothly… Otherwise, please check the error provided in the test log. 
            WebDriver often prints and informative messages there.  
           
            Let me explain what had happened. 
            The class `SwdBrowser`  is defined in the namespace Swd.Core.WebDriver. 
            This class controls the WebDriver lifetime. 
            
            When you call `SwdBrowser.Driver`, it creates a new WebDriver instance. 
            When you call `SwdBrowser.Driver` for the second time – it returns the WebDriver instance 
            which was already created. That technique is called “lazy initialization”. 
            
            SwdBrowser is an global object. So you can request the “current” WebDriver instance 
            from any line of your code.

        */

        /*  Framework configuration file
            ----------------------------
           
            Now, the time had come to discuss the configuration files. In the Solution Explorer, please find and expand the folder “Configuration”. 
            This folder contains the files “App.config” and “Config.config”. 
            I know, “Config.config” is kind of a silly name… anyway, this is our main configuration file. 
            Please, open the file.
            The section we need to change is `Browser Name`, which defines the default browser. 
           
            This section should look like the following: 
            
            <!--==== Browser Name-->
              <add key="swdBrowserType" value="Firefox"/>
              <!--<add key="swdBrowserType" value="InternetExplorer"/>-->
              <!--<add key="swdBrowserType" value="Chrome"/>-->
            
            Now, comment the line with “Firefox” and uncomment "Chrome". 
            Note: In the xml files (and Config.config is an xml file), to comment the line you need to put it inside:
           
           <!-- -->
           
            In Visual Studio, there are a convenient hotkeys to comment/uncomment lines. 
            To comment the line (in the C# code or in the XML/HTML file), put caret on the line you want to comment; hold `Ctrl` and press K, C. (`Ctr+K,C`)
            To uncomment line – press `Ctrl+K,U`. 
          
            After this task is done, the section should look like the following: 
          
            <!--==== Browser Name-->
              <!--<add key="swdBrowserType" value="Firefox"/>-->
              <!--<add key="swdBrowserType" value="InternetExplorer"/>-->
              <add key="swdBrowserType" value="Chrome"/>

            And one more important step!
            SWD.Starter does not include the driver executable. You need to download  "chromedriver.exe" and "IEDriverServer.exe" separately. 
            Please, visit Selenium Download page: http://www.seleniumhq.org/download/
            And download the driver executable for Internet Explorer and Chrome. 
            
            Extract and put those *.exe files  into the folder SWD.Starter\webdrivers\
            Swd.Starter
             +---bin
             +---docs
             +---src
             +---tools
             \---webdrivers
                     chromedriver.exe
                     IEDriverServer.exe
                     readme.txt
          


            After you’ll be done downloading the files, please, run the test below. 
            The test should open a local HTML file in the Chrome browser. 

            Reminder: Inside the TestMethod code, right-click to invoke Context Menu, 
                      and choose "Run Tests"
        */

        [TestMethod]
        public void S10_Change_the_configuration_and_open_local_page_in_Chrome()
        {
            // Btw, the method Ch00_OpenHtmlFile, uses SwdBrowser.Driver inside
            DemoPages.Ch00_OpenHtmlFile("simple.html");

            Console.Write(SwdBrowser.Driver.Title);
            Console.Write("===========================");
            Console.Write(SwdBrowser.Driver.PageSource);

            System.Threading.Thread.Sleep(5 * 1000); // Sleep for 5 seconsds
        }

    }
}
