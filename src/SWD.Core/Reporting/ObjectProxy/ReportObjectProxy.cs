using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace Swd.Core.Reporting.ObjectProxy
{
    public class ReportObjectProxy<T> where T : class, ISupportReporting 
    {
        private T _object;

        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public T MakeObject()
        {
            return ProxyGenerator.CreateClassProxyWithTarget<T>(_object, new ClassInterceptor());
        }

        public ReportObjectProxy(T instance)
        {
            _object = instance;
        }
    }

}
