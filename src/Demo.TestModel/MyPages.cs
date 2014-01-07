using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.TestModel.PageDeclarations;

namespace Demo.TestModel
{
    public static class MyPages
    {

        // This method can be extended to support additional logic for all pages, 
        // for instance, switch to the frame or window when it would be required or cache 
        // the created pageobject instance
        public static T GetPage<T>() where T : MyPageBase, new()
        {
            T page = new T();
            return page;
        }


        // This is an example for adding pages into MyPages. 
        // Please, notice the property name is same as returning type; 
        //            /* TYPE       */ /* NAME       */
        public static DummyExamplePage DummyExamplePage { get { return GetPage<DummyExamplePage>(); } }
        public static EmptyPage        EmptyPage        { get { return GetPage<EmptyPage>(); } }

        // Put your new pages here: 
        //=======================================================================================

        public static CreateNewAccountPage CreateNewAccountPage { get { return GetPage<CreateNewAccountPage>(); } }

    }
}
