using System;
using System.Web;
using System.Collections.Generic;
using VKeCRM.Common.Logging;

namespace VKeCRM.Framework.Web.State
{
    /// <summary>
    /// Use this class to manage access to data stored in the Application Bag or the Cache. 
    /// If you use the Application bag make sure you synchronize access to it using lock/unlock.
    /// </summary>
    public class ApplicationState
	{
		#region Application

        /// <summary>
        /// Returns a reference to the current Application object. Should not really be used unless 
        /// absolutely necessary.
        /// </summary>
        public static HttpApplicationState Application
        {
            get
            {
                return HttpContext.Current.Application;
            }
        }

		#endregion

        protected static object GetApplicationValue(string key)
        {
            return GetApplicationValue(key, null);    
        }

        protected static object GetApplicationValue(string key, object defaultValue)
        {
            object result = defaultValue;

            Application.Lock();

            try
            {
                result = Application[key];
            }
            catch (Exception)
            {
                // use default value
            }
            finally
            {
                Application.UnLock();
            }

            return result;
        }

        protected static void SetApplicationValue(string key, object value)
        {
            Application.Lock();

            try
            {
                Application[key] = value;
            }
            finally
            {
                Application.UnLock();
            }
        }

		#region Init

        /// <summary>
        /// Call this method to initialize Common Application state
        /// </summary>
        public static void Init()
        {
            // If using the Application Bag, lock it
            //Application.Lock();

            // Initialize Application Bag as needed

            // If using the Application Bag, unlock it
            //Application.UnLock();
		}

		#endregion

		#region Constructors

        /// <summary>
        /// No need to construct, all properties should be public, static.
        /// </summary>
        protected ApplicationState()
        {
		}

		#endregion

        #region LoggerManager

        /// <summary>
        /// LoggerManager only used by ControllerBase to provide access to the CommonLogger.
        /// All other loggers are access through the application specific LoggerManager.
        /// </summary>
        public static LoggerManager LoggerManager
        {
            get
            {
                return GetApplicationValue(ApplicationStateKey.LoggerManager) as LoggerManager;
            }
        }

        public static Dictionary<string, string> SiteUnmapUrlDictionary
        {
            get
            {
                return GetApplicationValue(ApplicationStateKey.SiteUnmapUrlDictionary) as Dictionary<string, string>;
            }
            set
            {
                SetApplicationValue(ApplicationStateKey.SiteUnmapUrlDictionary, value);
            }
        }

        #endregion // LoggerManager

    }
}
