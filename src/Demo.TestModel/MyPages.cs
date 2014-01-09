using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.TestModel.PageDeclarations;

namespace Demo.TestModel
{
    /**   
        The class `MyPages` is an entry point for your PageObjects.   \n
        This is a Factory class, which is accessible from any place of the code.  \n
        Allows to call PegeObject actions from tests as well as from another PaeObjects   \n
    */
    public static class MyPages
    {

        /**
            This method can be extended to support additional logic for all pages, 
            for instance, switch to the frame or window when it would be required or cache 
            the created pageobject instance 
        */
        public static T GetPage<T>() where T : MyPageBase, new()
        {
            T page = new T();
            return page;
        }

        /**
            Adding new pages
            ----------------
            For instance, you have defined a new PageObject with name `UserProfilePage` \n
            In order to have a convenient access to this page from any place of the test automation code, like this one: 

            > MyPages.UserProfilePage.DoSomeAction(“param1”);

            Put the line in the following format to the list below: 

            >  public static UserProfilePage UserProfilePage { get { return GetPage<UserProfilePage>(); } }

            Please, don't be confused because the property name and returning type are same. It is allowed in C#, but, anyway, you can give any name for the property. 
        */

        // Example:
        public static DummyExamplePage DummyExamplePage { get { return GetPage<DummyExamplePage>(); } }
        public static EmptyPage        EmptyPage        { get { return GetPage<EmptyPage>(); } }

        // Put your new pages here: 
        //=======================================================================================

        public static CreateNewAccountPage CreateNewAccountPage { get { return GetPage<CreateNewAccountPage>(); } }

    }
}
