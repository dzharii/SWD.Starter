SWD.Starter
===========

## :apple: Installation Prerequisites 
In order to download and start using the project you have to install:  

* **[Git]( http://git-scm.com/)** 
* [Visual Studio 2013 Express Desktop]( http://www.microsoft.com/en-US/download/details.aspx?id=40787) (if you don’t have a professional one) 

I personally recommend to use [Chocolatey]( https://chocolatey.org/) in order to perform such boring operations – automatically.

## :small_red_triangle_down: Download SWD Page Recorder

[![Logo](https://github.com/dzharii/SWD.Starter/raw/master/images/page-recorder.png)](https://github.com/dzharii/swd-recorder/releases)


## Inaccurate instructions

    1. Run Page Recorder
    2. Open SWD.Starter / src / SWD.StarterKit.sln
    	2.1 Copy 
    		* chromedriver.exe
    		* IEDriverServer.exe
    		into folder webdrivers
    	
    	2.2 Build the project

    3. Record your PageObject with Page Recorder
      
    4. Generate code with template "[CSharp] SWD Starter PageObjects"
    5. Follow the instructions inside generated code
    	5.1 Implement Invoke() and IsDisplayed();

    6. Run the tests (set browser in the file "Config.config")
       swdBrowserType
       default is Firefox


## Doxygen documentation generator

Just don’t forget to run `SWD.Starter\doxygen_run.cmd` and enjoy your framework API documentation!  

See generated `SWD.Starter\docs\api\html\index.html`

![Logo](https://github.com/dzharii/SWD.Starter/raw/master/images/doxy.png)

## Core concepts: 

1. Share the opened browser instance across different test cases – because that is how the most users do, e.g. they do not close the browser after each action.  
1. If you really want parallel test execution, just run parallel processes… Multithreaded tests implementation inside a single process would add complexity to the code and kill your workstation performance.  
1. Start using PageObjects – or your code will turn to real crap soon.   
1. Are your 3000 UI tests took too much time? – You are doing something wrong when you want to test everything through UI. Consider 20 high level (UI) to 80 low level (HTTP Requests / Database / Unit) tests ratio.   
1. Decouple your code: avoid copy-paste approach as long as it is reasonable: 
   * When the duplicated code in unique only for the given suite – create a separate method inside the suite
   * When the duplicated code can be used globally across the project – move it to a special BusinessSteps class or to the specific page object class; 
   * when you code just extends WebDriver functionality and has no connection to your test project logic – move it to the framework core assembly. 

1. When you work with PageObject, it is OK to create a lot of small methods with business logic oriented names. E.g., if you have the code, which opens a new project form: 
```
btnNewProject.Click();
WaitForAjax();
```
Then just move it to a separate PageObject method: 
```
var newProjectForm = projectsPage.OpenNewProjectForm();
```
1. Be proud of your code


## :books: Useful Materials

### :blue_book: Test Automation Framework methodology 


:warning: read this only if you understand Russian... Otherwise... you will not understand anything. LOL: **[Заметки по архитектуре и методологии фреймворка](https://github.com/dzharii/SWD.Starter/blob/master/methodology_all_in_one_rus.md)**


### :anchor: External Articles in English:

* [SWD Page Recorder – records WebElements and generates PageObject classes (Announcement)](https://groups.google.com/d/msg/selenium-users/epneoHaOymk/MjjhyoBcUf4J)
* [PageObject Generator Utility for Selenium WebDriver](http://unmesh.me/2013/08/29/pageobject-generator-utility-for-selenium-webdriver/)

### :anchor: External Articles in Russian:

* [Материалы моего доклада (SWD Page Recorder) на #SeleniumCamp 2014 и ещё несколько фактов]( http://blog.zhariy.com/2014/02/swd-page-recorder-seleniumcamp-2014.html)
* [SWD Page Recorder: Записывает PageObject-классы для Selenium WebDriver]( http://habrahabr.ru/post/191802/)
* [SWD.Starter: Быстрый старт автоматизации тестирования UI на C# + Selenium WebDriver + PageObjects]( http://habrahabr.ru/post/208822/)


### :video_camera: Videos

* [(English)SWD Page Recorder: Working with Frames and JavaScript popups]( https://www.youtube.com/watch?v=C4jnX0PF_mc)
* [(Rus) SWD Page Recorder BETA1 -- записывает PageObject'ы на C#, Java, Ruby, Perl, Python!]( https://www.youtube.com/watch?v=4Md_kC4Fdpg)
* [За пределами PageObject](http://blog.zhariy.com/2013/02/atdays-pageobject.html)

### :octocat: Original Github open-source Projects

* [dzharii / swd-recorder](https://github.com/dzharii/swd-recorder)
* [dzharii / SWD.Starter](https://github.com/dzharii/SWD.Starter)

### :mag_right: About Dmytro ;)

* Blog (Rus): http://blog.zhariy.com/
* LinkedIn: https://www.linkedin.com/in/dmytrozharii
* Skype: dzhariy
* :corn: Resume:  https://drive.google.com/file/d/0Bx--97zzjKNERnZfdjNMdlVMUG8/edit?usp=sharing
* Personal Email: dzhariy !@[@]@! gmail.com
* :bird: Twitter: [@dzhariy](https://twitter.com/dzhariy)

