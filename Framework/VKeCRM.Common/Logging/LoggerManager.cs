//-----------------------------------------------------------------------
// <copyright file="LoggerManager.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;

namespace VKeCRM.Common.Logging
{
    /// <summary>
    /// Initializes a new instance of the LoggerManager class. This class is 
    /// the base for application logger managers. An application should 
    /// implement a logger manager that has properties that map to each 
    /// functional area the application accesses. The properties merely return 
    /// the result of a call to the Logger method that retrieves ancd caches
    /// the requested logger. 
    /// </summary>
    public class LoggerManager
	{
		#region Fields
		/// <summary>
		/// Holds the singleton instance of LoggerManager
		/// </summary>
		private static LoggerManager _loggerManager;

        /// <summary>
        /// Dictionary of loaded loggers
        /// </summary>
        private Dictionary<string, Logger> _loggers;

		/// <summary>
		/// Use to lock thread when updating the _loggers.
		/// </summary>
		private object _locker = new object();

		#endregion

		#region Properties
		/// <summary>
		/// Gets the Logger for logging exception related to Application Settings functional area
		/// </summary>
		public static Logger ApplicationSettingLogger
		{
			get
			{
				return GetLogger("ApplicationSettingService");
			}
		}

		/// <summary>
		/// Gets the Logger for logging exception related to Community Connection functional area
		/// </summary>
		public static Logger CommunityConnectionLogger
		{
			get
			{
				return GetLogger("CommunityConnectionService");
			}
		}

		/// <summary>
		/// Gets the Logger for logging exception related to Community Connection functional area
		/// </summary>
		public static Logger CommunityDiscussionLogger
		{
			get
			{
				return GetLogger("CommunityDiscussionService");
			}
		}

		/// <summary>
		/// Gets the Logger for logging exception related to Community Connection functional area
		/// </summary>
		public static Logger CommunityIntelligenceLogger
		{
			get
			{
				return GetLogger("CommunityIntelligenceService");
			}
		}

		/// <summary>
		/// Gets the Logger for logging exception related to Content Editor functional area
		/// </summary>
		public static Logger ContentEditorLogger
		{
			get
			{
				return GetLogger("ContentEditorService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Mailbox functional area
		/// </summary>
		public static Logger MailboxLogger
		{
			get
			{
				return GetLogger("MailboxService");
			}
		}

		/// <summary>
		/// Gets the Logger for logging exception related to Member functional area
		/// </summary>
		public static Logger MemberLogger
		{
			get
			{
				return GetLogger("MemberService");
			}
		}

		/// <summary>
		/// Gets the Logger for logging exception related to Membership Provider functional area
		/// </summary>
		public static Logger MembershipProviderLogger
		{
			get
			{
				return GetLogger("MembershipProviderService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Messaging functional area
		/// </summary>
		public static Logger MessagingLogger
		{
			get
			{
				return GetLogger("MessagingService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to NHibernate Second Level Cache functional area
		/// </summary>
		public static Logger NHibernateSecondLevelCacheLogger
		{
			get
			{
				return GetLogger("NHibernateSecondLevelCacheService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Member Portfolio functional area
		/// </summary>
		public static Logger PortfolioLogger
		{
			get
			{
				return GetLogger("PortfolioService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Member Profile functional area
		/// </summary>
		public static Logger ProfileLogger
		{
			get
			{
				return GetLogger("ProfileService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Quote functional area
		/// </summary>
		public static Logger QuoteLogger
		{
			get
			{
				return GetLogger("QuoteService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Role Provider functional area
		/// </summary>
		public static Logger RoleProviderLogger
		{
			get
			{
				return GetLogger("RoleProviderService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Security Challenge functional area
		/// </summary>
		public static Logger SecurityChallengeLogger
		{
			get
			{
				return GetLogger("SecurityChallengeService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Community Server Discussions functional area
		/// </summary>
		public static Logger DiscussionsLogger
		{
			get
			{
				return GetLogger("DiscussionsService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Community Server Admin functional area
		/// </summary>
		public static Logger CSAdminLogger
		{
			get
			{
				return GetLogger("AdminService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Portal Web functional area
		/// </summary>
		public static Logger PortalWebLogger
		{
			get
			{
				return GetLogger("Portal.Web");
			}
		}

        public static Logger KickOutInfoLogger
        {
            get
            {
                return GetLogger("KickOutInfo");
            }
        }

		/// <summary>
		/// Gets the logger for logging exception related to Portal Admin Web functional area
		/// </summary>
		public static Logger PortalAdminWebLogger
		{
			get
			{
				return GetLogger("Portal.Admin.Web");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Trading Web functional area
		/// </summary>
		public static Logger TradingWebLogger
		{
			get
			{
				return GetLogger("Trading.Web");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to GainsKeeperService area
		/// </summary>
		public static Logger GainsKeeper
		{
			get
			{
				return GetLogger("GainsKeeperService");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to OLA Web functional area
		/// </summary>
		public static Logger OLALogger
		{
			get
			{
				return GetLogger("Ola");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to OLA Web functional area
		/// </summary>
		public static Logger OLACommonLogger
		{
			get
			{
				return GetLogger("OlaCommon");
			}
		}
		
		/// <summary>
		/// Gets the logger for logging exception related to Jsam service functional area
		/// </summary>
		public static Logger JsamService
		{
			get
			{
				return GetLogger("JsamService");
			}
		}

		#region Performance tuning loggers
		/// <summary>
		/// Gets the logger for performance tuning
		/// </summary>
		//public static Logger TraceLogger
		//{
		//    get
		//    {
		//        return GetLogger("PerformanceTuning");
		//    }
		//}
		/// <summary>
		/// Gets the logger for page request start
		/// </summary>
		public static Logger TracePageStartLogger
		{
			get
			{
				return GetLogger("Trace.Page.Start");
			}
		}
		/// <summary>
		/// Gets the logger for page request end
		/// </summary>
		public static Logger TracePageEndLogger
		{
			get
			{
				return GetLogger("Trace.Page.End");
			}
		}
		/// <summary>
		/// Gets the logger for Service request start
		/// </summary>
		public static Logger TraceServiceClientStartLogger
		{
			get
			{
				return GetLogger("Trace.Service.Client.Start");
			}
		}
		/// <summary>
		/// Gets the logger for Service request end
		/// </summary>
		public static Logger TraceServiceClientEndLogger
		{
			get
			{
				return GetLogger("Trace.Service.Client.End");
			}
		}
		/// <summary>
		/// Gets the logger for Service request start
		/// </summary>
		public static Logger TraceServiceServerStartLogger
		{
			get
			{
				return GetLogger("Trace.Service.Server.Start");
			}
		}
		/// <summary>
		/// Gets the logger for Service request end
		/// </summary>
		public static Logger TraceServiceServerEndLogger
		{
			get
			{
				return GetLogger("Trace.Service.Server.End");
			}
		}
		#endregion

		/// <summary>
		/// Gets the logger 
		/// </summary>
		public static Logger ServiceModelLogger
		{
			get
			{
				return GetLogger("ServiceModelLogger");
			}
		}

        /// <summary>
        /// Gets the logger for logging exception related to Jsam service functional area
        /// </summary>
        public static Logger ApiWeb
        {
            get
            {
                return GetLogger("Api.Web");
            }
        }
        
        /// <summary>
        /// Gets the logger for logging exception related to Jsam service functional area
        /// </summary>
        public static Logger TapiWeb
        {
            get
            {
                return GetLogger("Tapi.Web");
            }
        }

		/// <summary>
		/// Gets the logger for logging exception related to facebook area
		/// </summary>
		public static Logger FacebookWeb
		{
			get
			{
				return GetLogger("Facebook.Web");
			}
		}

		/// <summary>
		/// Gets the logger for logging exception related to Group functional area
		/// </summary>
		public static Logger GroupLogger
		{
			get
			{
				return GetLogger("GroupService");
			}
		}

        /// <summary>
        /// Gets the logger for logging exception related to Nexa function
        /// </summary>
        public static Logger NexaLogger
		{
			get
			{
				return GetLogger("NexaLogFileAppender");
			}
		}

        /// <summary>
        /// Gets the logger for logging exception related to Hermes function
        /// </summary>
        /// <summary>
        /// Gets the logger for logging exception related to Hermes function
        /// </summary>
        public static Logger HermesLogger
        {
            get
            {
                return GetLogger("Hermes");
            }
        }

        /// <summary>
        /// Gets the logger for logging Hermes trade activities
        /// </summary>
        public static Logger HermesActivityLogger
        {
            get
            {
                return GetLogger("HermesActivity");
            }
        }

		/// <summary>
		/// Gets the logger for logging exception related to Hermes Auto Test
		/// </summary>
		public static Logger HermesPensonAutoTestLogger
		{
			get
			{
				return GetLogger("HermesPensonAutoTest");
			}
		}
                
        /// <summary>
        /// Gets the logger for logging exception related to Nexa function
        /// </summary>
        public static Logger ApolloLogger
        {
            get
            {
                return GetLogger("ApolloLogFileAppender");
            }
        }

		/// <summary>
		/// Gets the logger for logging Apollo trade activities
		/// </summary>
		public static Logger ApolloActivityLogger
		{
			get
			{
				return GetLogger("ApolloActivity");
			}
		}

		/// <summary>
		/// Gets the logger of zao downloading
		/// </summary>
		public static Logger ZapDownloadLogger
		{
			get
			{
				return GetLogger("ZapDownload");
			}
		}

        /// <summary>
        /// Gets the logger for logging exception related to ApplyAdmin site
        /// </summary>
        public static Logger ApplyAdminLogger
        {
            get
            {
                return GetLogger("ApplyAdmin");
            }
        }

		/// <summary>
		/// Gets the logger of zao downloading
		/// </summary>
		public static Logger VKeCRMCommonLogger
		{
			get
			{
				return GetLogger("VKeCRMCommon");
			}
		}
        #endregion

		#region Constructors
		/// <summary>
        /// Initializes a new instance of the LoggerManager class. 
        /// Initializes the internal list of loggers.
        /// </summary>
        protected LoggerManager()
        {
            _loggers = new Dictionary<string, Logger>();
		}

		/// <summary>
		/// Initialize the singleton instance of the LoggerManager
		/// </summary>
		static LoggerManager()
		{
			if (_loggerManager == null)
			{
				_loggerManager = Nested.LoggerManager;
				_loggerManager.Init(@"c:\Logs\log4net.config");
			}
		}
		#endregion

		#region Methods
		#region Public Methods
		/// <summary>
        /// LoggerMannager implements this method to initialize
        /// Log4Net with the location of the xml configuration file.
        /// If an application overrides this it must call the base 
        /// version first.
        /// </summary>
        /// <param name="fileName">The fully qualified file name of the Log4Net configuration file.</param>
        public virtual void Init(string fileName)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(fileName));
		}
		#endregion

		#region Protected Methods
		/// <summary>
        /// Retireves a logger for a given functional area.
        /// </summary>
        /// <param name="functionalArea">The name of the functional area to retrieve a logger.</param>
        /// <returns>The logger for the desired functional area.</returns>
        protected Logger Logger(string functionalArea)
        {
            Logger result;

            if (_loggers.ContainsKey(functionalArea))
            {
                result = _loggers[functionalArea];
            }
            else
            {
                result = new Logger(functionalArea);

				lock (_locker)
				{
					_loggers[functionalArea] = result;
				}
              
            }

            return result;
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// Retireves a logger for a given functional area.
		/// </summary>
		/// <param name="functionalArea">The name of the functional area to retrieve a logger.</param>
		/// <returns>The logger for the desired functional area.</returns>
		public static Logger GetLogger(string functionalArea)
		{
			return _loggerManager.Logger(functionalArea);
		}
		#endregion
		#endregion

		#region Nested Class
		/// <summary>
		/// Fully lazy initialization of the static member: triggered by the 
		/// first reference. Implementation has performance benefits, and is 
		/// fully lazy.
		/// </summary>
		/// <remarks>
		/// http://www.yoda.arachsys.com/csharp/singleton.html
		/// </remarks>
		private class Nested
		{
			/// <summary>
			/// read only instance of JobEngine
			/// </summary>
			internal static LoggerManager LoggerManager = new LoggerManager();

			/// <summary>
			/// Initializes static members of the Nested class
			/// </summary>
			static Nested()
			{
				// Explicit static constructor to tell C# compiler to not mark type as beforefieldinit
			}
		}
		#endregion
    }
}
