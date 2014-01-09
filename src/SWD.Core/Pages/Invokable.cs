using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swd.Core.Pages
{
    public interface Invokable
    {
        void Invoke();
        bool IsDisplayed();
    }
}
