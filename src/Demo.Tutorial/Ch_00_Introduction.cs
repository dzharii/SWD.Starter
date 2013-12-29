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

        /* Framework configuration file
           ----------------------------
           
        */

    }
}
