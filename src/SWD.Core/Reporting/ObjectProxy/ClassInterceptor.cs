using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.DynamicProxy;

namespace Swd.Core.Reporting.ObjectProxy
{
    public class ClassInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
