using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Swd.Core.Configuration
{
    public class Config
    {
        public static bool wdIsRemote
        {
            get
            {
                return ConfigurationManager.AppSettings["swdIsRemote"] == "true";
            }
        }
        public static string wdRemoteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["swdRemoteUrl"];
            }
        }
        public static string swdBrowserType
        {
            get
            {
                return ConfigurationManager.AppSettings["swdBrowserType"];
            }
        }
    }
}
