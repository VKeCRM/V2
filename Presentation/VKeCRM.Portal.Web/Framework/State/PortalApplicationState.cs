using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VKeCRM.Framework.Web.State;
using VKeCRM.Portal.Web.Framework.Logging;

namespace VKeCRM.Portal.Web.Framework.State
{
    /// <summary>
    /// Use this class to manage access to data stored in the Application Bag or the Cache. 
    /// If you use the Application bag make sure you synchronize access to it using lock/unlock.
    /// This class adds only Portal specific data.
    /// </summary>
    public sealed class PortalApplicationState : ApplicationState
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the TradingApplicationState class from being created
        /// </summary>
        private PortalApplicationState()
        {
            //// Contructor PortalApplicationState
        }

        #endregion

        #region Init

        /// <summary>
        /// Call this method to initialize Common and Portal Application state
        /// </summary>
        public static new void Init()
        {
            //// Initialize Common Application State
            ApplicationState.Init();
            //// Initialize Portal Application State if needed
            //// Initialize Portal Application State if needed
            LoggerManager = new PortalLoggerManager();
            LoggerManager.Init(@"c:\Logs\log4net.config");
        }
        #endregion

        #region Portal Application Properties
        new public static PortalLoggerManager LoggerManager
        {
            get
            {
                PortalLoggerManager loggerManager = GetApplicationValue(ApplicationStateKey.LoggerManager) as PortalLoggerManager;
                if (loggerManager == null)
                {
                    loggerManager = new PortalLoggerManager();
                    loggerManager.Init(@"c:\Logs\log4net.config");
                    SetApplicationValue(ApplicationStateKey.LoggerManager, loggerManager);
                }
                return loggerManager;
            }

            set
            {
                if (value != null)
                {
                    SetApplicationValue(ApplicationStateKey.LoggerManager, value);
                }
            }
        }
        #endregion // Portal Application Properties
    }
}