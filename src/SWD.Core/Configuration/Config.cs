using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Swd.Core.Configuration
{
    /// <summary>
    ///  Config -- Core Configuration Class
    /// </summary>
    
    #region Documentation
    /// <summary> 
    /// Framework Configuration Files
    /// -----------------------------
    /// The configuration class in the Swd.Starter allows to change some values without project recompilation, such as:
    ///
    /// * Selecting WebBrowser
    /// * Setting the main application  URLs and others;
    ///
    /// The configuration sub-module is based on standard .NET Framework XML configuration files with several tweaks. 
    ///
    /// In the project Src folder, you can find the following files:
    /// * App.config – contains a technical configuration and includes the file Config.config. (See line <appSettings configSource="Config.config"/>)
    /// * Config.config – contains user-defined settings.
    ///
    /// The class `Config` read the configuration values from the class `Config.config` \n
    ///
    ///
    /// Those two configuration files are shared between all the projects. 
    ///
    /// How to properly add configuration files into  the new project
    /// -----------------------------------------------------------------------------------------
    /// For instance, you have created new testing projects `MyApp.TestModel` and MyApp.Tests. 
    ///
    /// In order to add Global Configuration Files support, please follow the instructions below: 
    ///
    /// 1. Open Right-click context menu for project `MyApp.TestModel` in the Solution Explorer
    /// 2. Pick item Add → Existing Item...
    /// 3. In the Open File fialog, find the file `App.config` in the folder `SWD.Starter\src`
    /// 4. Select the file, but do not press button “Add” yet
    /// 5. Clich on the right side of the Add button in order to Invoke Add popup menu from
    /// 6. Pick “Add as a Link”
    /// 7. Recompile the project
    ///
    /// Repeat the steps for `MyApp.Tests`
    /// <summary> 

    #endregion

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

        /// <summary>
        /// Add your own configuration parameters
        /// -------------------------------------
        /// Quick instructions: \n
        ///
        /// 1. Open the file Config.config
        /// 2. Add the following xml line: 
        ///    > <add key="yourParameterName" value="AnyValue"/>
        /// 3. Create a property with name “yourParameterName” which reads configuration file key yourParameterName
        /// </summary>
        /// 
        /// Put your configuration settings below:

        public static string yourParameterName
        {
            get
            {
                return ConfigurationManager.AppSettings["yourParameterName"];
            }
        }        

    }

}
