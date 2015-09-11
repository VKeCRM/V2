using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Web;
using log4net;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Impl;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using NHibernate.Transaction;

namespace VKeCRM.Framework.Data
{
    /// <summary>
    /// Handles creation and management of sessions and transactions.  It is a singleton because 
    /// building the initial session factory is very expensive. Inspiration for this class came 
    /// from Chapter 8 of Hibernate in Action by Bauer and King.  Although it is a sealed singleton
    /// you can use TypeMock (http://www.typemock.com) for more flexible testing.
    /// </summary>
    public sealed class NHibernateSessionManager
    {
        #region Declarations
        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        private const string SESSION_KEY = "CONTEXT_SESSION";
        private ISessionFactory sessionFactory;        
        private const string DIRTY_FLAG_KEY = "SESSION_DIRTY_FLAG";
        //  This flag indicates whether we use ReadUncommitted isolation level when getting session in first time, the default value is true
        private static bool IsDirtyReadSession = bool.Parse(ConfigurationManager.AppSettings["NHibernate.Session.DirtyRead"] ?? "true");
        private static ILog logger = LogManager.GetLogger(typeof(NHibernateSessionManager));

        #endregion

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static NHibernateSessionManager Instance
        {
            get
            {
                return Nested.NHibernateSessionManager;
            }
        }

        /// <summary>
        /// Initializes the NHibernate session factory upon instantiation.
        /// </summary>
        private NHibernateSessionManager()
        {
            InitSessionFactory();
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested()
            {
            }
            internal static readonly NHibernateSessionManager NHibernateSessionManager =
                new NHibernateSessionManager();
        }

        #endregion

        /// <summary>
        /// Allows you to register an interceptor on a new session.  This may not be called if there is already
        /// an open session attached to the HttpContext.  If you have an interceptor to be used, modify
        /// the HttpModule to call this before calling BeginTransaction().
        /// </summary>
        public void RegisterInterceptor(IInterceptor interceptor)
        {
            ISession session = ContextSession;

            if (session != null && session.IsOpen)
            {
                throw new CacheException("You cannot register an interceptor once a session has already been opened");
            }

            GetSession(interceptor);
        }

        /// <summary>
        /// Get current session.
        /// If it's null, then create new session which default isolation level is ReadCommitted and without transaction
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return GetSession(null);
        }

        /// <summary>
        /// Gets a session with or without an interceptor.  This method is not called directly; instead,
        /// it gets invoked from other public methods.
        /// </summary>
        private ISession GetSession(IInterceptor interceptor)
        {
            ISession session = ContextSession;

            // open new session
            if (session == null)
            {
                if (interceptor != null)
                {
                    session = sessionFactory.OpenSession(interceptor);
                }
                else
                {
                    session = sessionFactory.OpenSession();
                }

                // reset isolation level when getting session in first time
                var cmd = session.Connection.CreateCommand();
                cmd.CommandText = string.Format("SET TRANSACTION isolation level read {0}", IsDirtyReadSession ? "UNCOMMITTED" : "COMMITTED");
                cmd.ExecuteNonQuery();


                if (session == null && !session.IsOpen)
                {
                    throw new System.ApplicationException("session was null");
                }
                else
                {
                    ContextSession = session;

                    // we will no longer begin transaction by default. please use NHibernateTransaction attribute in wcf method to begin a transaction
                    //BeginTransaction();
                }
            }

            return session;
        }

        public IDbConnection GetDbConnection()
        {
            return (sessionFactory as NHibernate.Engine.ISessionFactoryImplementor).ConnectionProvider.GetConnection();
        }

        /// <summary>
        /// Close the session and release the connection
        /// we don't auto-flush session here.
        /// </summary>
        public void CloseSession()
        {
            ISession session = ContextSession;

            if (session != null)
            {
                session.Close();
                ResetFlags();
            }

            ContextSession = null;
        }

        /// <summary>
        /// begin a new transaction if there's no open transaction in current session
        /// the default isolation level it Readcommitted
        /// </summary>
        public void BeginTransaction()
        {
            if (!HasOpenTransaction())
            {
                var currentSession = ContextSession ?? GetSession();
                // the transaction's isolation level depends on "connection.isolation" setting of NHibernate, default value is ReadCommitted
                currentSession.BeginTransaction();
                logger.Debug(string.Format("Begin Transaction - create new transaction ({0}), isolation level = {1} ", currentSession.Transaction, ((AdoTransaction)currentSession.Transaction).IsolationLevel));
            }
            else
            {
                logger.Debug(string.Format("Begin Transaction - use the existed transaction ({0}), isolation level = {1} ", ContextSession.Transaction, ((AdoTransaction)ContextSession.Transaction).IsolationLevel));
            }
        }

        /// <summary>
        /// commit transaction if open transaction exists in current session
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (HasOpenTransaction())
                {
                    ContextSession.Transaction.Commit();
                    logger.Debug(string.Format("End Transaction - commit transaction ({0})", ContextSession.Transaction));
                }
                else if (IsDirty())
                {
                    throw new Exception(string.Format("Can't commit transaction cause no open transaction existed while the session is dirty - base flag={0}, customized flag={1}", ContextSession.IsDirty(), IsSessionDirty));
                }
            }
            catch
            {
                // Close session once exception happens
                CloseSession();
                throw;
            }
            finally
            {
                ResetFlags();
            }
        }

        /// <summary>
        /// rollback transaction if open transaction exists in current session
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (HasOpenTransaction())
                {
                    ContextSession.Transaction.Rollback();
                    logger.Debug(string.Format("End Transaction - rollback transaction ({0})", ContextSession.Transaction));
                }
                else if (IsDirty())
                {
                    throw new Exception(string.Format("Can't rollback transaction cause no open transaction existed while the session is dirty - base flag={0}, customized flag={1}", ContextSession.IsDirty(), IsSessionDirty));
                }
            }
            catch
            {
                // Close session once exception happens
                CloseSession();
                throw;
            }
            finally
            {
                ResetFlags();
            }
        }

        /// <summary>
        /// Indicate whether we have open transaction in current session
        /// </summary>
        /// <returns></returns>
        public bool HasOpenTransaction()
        {
            return ContextSession != null && ContextSession.Transaction.IsActive;
        }

        /// <summary>
        /// Enforce refresh data no cache 
        /// </summary>
        public void EnforceRefreshData()
        {
            GetSession().CacheMode = CacheMode.Refresh;
        }

        /// <summary>
        /// Set the flag indicating whether this session is dirty.
        /// That means this ISession contain any changes which must be synchronized with the database, and any Update/Insert/Delete SQL be executed if we flushed this session.
        /// </summary>
        public void SetSessionDirty()
        {
            IsSessionDirty = true;
        }

        /// <summary>
        /// This method is to tell us whether we have Insert/Update/Delete operation in current session.
        /// session.IsDirty can't tell us the truth when creating new domain, but in other case it works well,
        /// so we maintain a custom flag, and combine them to get the real IsDirty flag.
        /// </summary>
        /// <returns></returns>
        public bool IsDirty()
        {
            return ContextSession != null && ContextSession.IsOpen && (ContextSession.IsDirty() || IsSessionDirty);
        }

        #region private methods
        private void InitSessionFactory()
        {
            Configuration config = new Configuration().Configure();
            string expiration = ConfigurationManager.AppSettings["hibernate.cache.expiration"];
            if (!string.IsNullOrEmpty(expiration))
                config.Properties["expiration"] = expiration;
            sessionFactory = config.BuildSessionFactory();
        }

        /// <summary>
        /// If within a web context, this uses <see cref="HttpContext" /> instead of the WinForms 
        /// specific <see cref="CallContext" />.  Discussion concerning this found at 
        /// http://forum.springframework.net/showthread.php?t=572.
        /// </summary>
        private ISession ContextSession
        {
            get
            {
                if (IsInWebContext())
                {
                    return (ISession)HttpContext.Current.Items[SESSION_KEY];
                }
                else
                {
                    return (ISession)CallContext.GetData(SESSION_KEY);
                }
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[SESSION_KEY] = value;
                }
                else
                {
                    CallContext.SetData(SESSION_KEY, value);
                }
            }
        }

        /// <summary>
        /// A flag indicate whether this ISession contain any changes which must be synchronized with the database, and any Update/Insert/Delete SQL be executed if we flushed this session.
        /// </summary>
        private bool IsSessionDirty
        {
            get
            {
                object dirtyFlag;
                if (IsInWebContext())
                {
                    dirtyFlag = HttpContext.Current.Items[DIRTY_FLAG_KEY];
                }
                else
                {
                    dirtyFlag = CallContext.GetData(DIRTY_FLAG_KEY);
                }

                return dirtyFlag != null ? (bool)dirtyFlag : false;
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[DIRTY_FLAG_KEY] = value;
                }
                else
                {
                    CallContext.SetData(DIRTY_FLAG_KEY, value);
                }
            }
        }

        private bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }

        /// <summary>
        /// reset all flags in this singleton class
        /// </summary>
        private void ResetFlags()
        {
            IsSessionDirty = false; // Reset the flag to indicate no updates
        }
        #endregion
    }
}
