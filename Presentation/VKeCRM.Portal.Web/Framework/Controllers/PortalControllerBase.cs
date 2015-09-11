//-----------------------------------------------------------------------
// <copyright file="TradingControllerBase.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using VKeCRM.Framework.Web.Exceptions;
using VKeCRM.Portal.Web.Framework.Logging;
using VKeCRM.Portal.Web.Framework.State;

namespace VKeCRM.Portal.Web.Framework.Controllers
{
    /// <summary>
    /// Root for all controller objects.
    /// </summary>
    public abstract class PortalControllerBase : VKeCRM.Framework.Web.Controllers.ControllerBase
    {
        #region LoggerManager

        private static PortalLoggerManager _manager;
        new public static PortalLoggerManager Logging
        {
            get
            {
                // Load it if it has not been loaded
                if (_manager == null)
                {
                    // Load LoggerManager from Application Bag
                    _manager = PortalApplicationState.LoggerManager;

                    // Raise an exception if it has not been initialized.
                    if (_manager == null)
                        throw new LoggerManagerNotInitializedException();
                }

                // return it
                return _manager;
            }
        }

        #endregion // LoggerManager

        #region Error Handling
        public string ProcessException(string messageToDisplay, Exception ex)
        {
            return ProcessException(messageToDisplay, ex, PortalLoggerManager.PortalWebLogger);
        }
        #endregion
    }
}
