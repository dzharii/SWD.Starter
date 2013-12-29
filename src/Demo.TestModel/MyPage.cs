using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swd.Core;
using Swd.Core.Pages;

namespace Demo.TestModel
{
    public abstract class MyPage : CorePage, Invokable
    {

        public abstract void Invoke();
    }
}
