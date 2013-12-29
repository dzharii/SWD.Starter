using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swd.Core.Reporting.ObjectProxy;

namespace Swd.Core.Reporting
{
    public class TestReport
    {
        public static T Init<T>(T instance) where T : class, ISupportReporting
        {
            return new ReportObjectProxy<T>(instance).MakeObject();
        }
    }
}
