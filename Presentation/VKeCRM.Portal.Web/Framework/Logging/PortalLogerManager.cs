using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VKeCRM.Common.Logging;

namespace VKeCRM.Portal.Web.Framework.Logging
{
    public class PortalLoggerManager:LoggerManager
    {
        /// <summary>
        /// Gets the Logger for logging exception related to Application Settings functional area
        /// </summary>
        public Logger ApplicationSettingLogger
        {
            get
            {
                return Logger("ApplicationSettingService");
            }
        }
    }
}