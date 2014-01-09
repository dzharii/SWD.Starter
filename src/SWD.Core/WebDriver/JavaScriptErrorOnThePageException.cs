using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swd.Core.WebDriver
{
    public class JavaScriptErrorOnThePageException : Exception
    {

        public JavaScriptErrorOnThePageException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
