//-----------------------------------------------------------------------
// <copyright file="Logger.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using log4net;

namespace VKeCRM.Common.Logging
{
    /// <summary>
    /// Initializes a new instance of the Logger class. Use this
    /// class to log messages.
    /// </summary>
    public class Logger
    {
		/// <summary>
		/// Holds the functional area logger object
		/// </summary>
		private ILog _logger;

		///// <summary>
		///// Debug logger
		///// </summary>
		//private ILog _debug;

		///// <summary>
		///// Error Logger
		///// </summary>
		//private ILog _error;

		///// <summary>
		///// Fatal Logger
		///// </summary>
		//private ILog _fatal;

		///// <summary>
		///// Info Logger
		///// </summary>
		//private ILog _info;

		///// <summary>
		///// Warning Logger
		///// </summary>
		//private ILog _warn;

        /// <summary>
        /// Initializes a new instance of the Logger class. Constructs an instance of the Logger class. 
        /// Requires the functional area name.
        /// </summary>
        /// <param name="functionalArea">the name of the functional area.</param>
        internal Logger(string functionalArea)
        {
			_logger = LogManager.GetLogger(string.Format("VKeCRM.{0}", functionalArea));
			//_debug = LogManager.GetLogger(string.Format("{0}.VKeCRM.{1}", "Debug", functionalArea));
			//_error = LogManager.GetLogger(string.Format("{0}.VKeCRM.{1}", "Error", functionalArea));
			//_fatal = LogManager.GetLogger(string.Format("{0}.VKeCRM.{1}", "Fatal", functionalArea));
			//_info = LogManager.GetLogger(string.Format("{0}.VKeCRM.{1}", "Info", functionalArea));
			//_warn = LogManager.GetLogger(string.Format("{0}.VKeCRM.{1}", "Warn", functionalArea));
        }

        /// <summary>
        /// Prevents a default instance of the Logger class from being created. Constructs 
        /// an instance of the Logger class.
        /// </summary>
        private Logger()
        {
        }

        #region // Debug

        /// <summary>
        /// Log a message object with the Debug level including
        /// the stack trace of the <see cref="T:System.Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object)"/>
        public void Debug(object message, System.Exception exception)
        {
			if (_logger.IsDebugEnabled)
            {
				_logger.Debug(message, exception);
            }
        }

        /// <overloads>Log a message object with the Debug level.</overloads>
        /// <summary>
        /// Log a message object with the Debug level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>DEBUG</c>
        /// enabled. If this logger is enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
        /// to this method will print the name of the <see cref="T:System.Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object,System.Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object,System.Exception)"/>
        public void Debug(object message)
        {
			if (_logger.IsDebugEnabled)
            {
				_logger.Debug(message);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object)"/>
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
			if (_logger.IsDebugEnabled)
            {
				_logger.DebugFormat(provider, format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object)"/>
        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
			if (_logger.IsDebugEnabled)
            {
				_logger.DebugFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object)"/>
        public void DebugFormat(string format, object arg0, object arg1)
        {
			if (_logger.IsDebugEnabled)
            {
				_logger.DebugFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object)"/>
        public void DebugFormat(string format, object arg0)
        {
			if (_logger.IsDebugEnabled)
            {
				_logger.DebugFormat(format, arg0);
            }
        }

        /// <overloads>Log a formatted string with the Debug level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Debug(System.Object)"/>
        public void DebugFormat(string format, params object[] args)
        {
			if (_logger.IsDebugEnabled)
            {
				_logger.DebugFormat(format, args);
            }
        }

        #endregion // Debug

        #region // Error

        /// <summary>
        /// Log a message object with the Error level including
        /// the stack trace of the <see cref="T:System.Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object)"/>
        public void Error(object message, System.Exception exception)
        {
            if (_logger.IsErrorEnabled)
            {
                if (exception == null) return;

				_logger.Error(message, exception);

                Error(message, exception.InnerException);
            }
        }

        /// <overloads>Log a message object with the Error level.</overloads>
        /// <summary>
        /// Log a message object with the Error level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>ERROR</c>
        /// enabled. If this logger is enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
        /// to this method will print the name of the <see cref="T:System.Exception"/> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object,System.Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object,System.Exception)"/>
        public void Error(object message)
        {
			if (_logger.IsErrorEnabled)
            {
				_logger.Error(message);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object)"/>
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
			if (_logger.IsErrorEnabled)
            {
				_logger.ErrorFormat(provider, format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object)"/>
        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
			if (_logger.IsErrorEnabled)
            {
				_logger.ErrorFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object)"/>
        public void ErrorFormat(string format, object arg0, object arg1)
        {
			if (_logger.IsErrorEnabled)
            {
				_logger.ErrorFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object)"/>
        public void ErrorFormat(string format, object arg0)
        {
			if (_logger.IsErrorEnabled)
            {
				_logger.ErrorFormat(format, arg0);
            }
        }

        /// <overloads>Log a formatted string with the Error level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception"/> object to include in the
        /// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object,System.Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:VKeCRM.Common.Logging.Logger.Error(System.Object)"/>
        public void ErrorFormat(string format, params object[] args)
        {
			if (_logger.IsErrorEnabled)
            {
				_logger.ErrorFormat(format, args);
            }
        }

        #endregion // Error

		#region // Info

		/// <summary>
		/// Log a message object with the Info level including
		/// the stack trace of the <see cref="T:System.Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// <para>
		/// See the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/> form for more detailed information.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		public void Info(object message, System.Exception exception)
		{
			if (_logger.IsInfoEnabled)
			{
				_logger.Info(message, exception);
			}
		}

		/// <overloads>Log a message object with the Info level.</overloads>
		/// <summary>
		/// Log a message object with the Info level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <remarks>
		/// <para>
		/// This method first checks if this logger is <c>Info</c>
		/// enabled. If this logger is enabled, then it converts the message object
		/// (passed as parameter) to a string by invoking the appropriate
		/// <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		/// proceeds to call all the registered appenders in this logger 
		/// and also higher in the hierarchy depending on the value of 
		/// the additivity flag.
		/// </para>
		/// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		/// to this method will print the name of the <see cref="T:System.Exception"/> 
		/// but no stack trace. To print a stack trace use the 
		/// <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/> form instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		public void Info(object message)
		{
			if (_logger.IsInfoEnabled)
			{
				_logger.Info(message);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the Info level.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		/// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		public void InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (_logger.IsInfoEnabled)
			{
				_logger.InfoFormat(provider, format, args);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the Info level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">The first object to format</param>
		/// <param name="arg1">The second object to format</param>
		/// <param name="arg2">The third object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		/// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		public void InfoFormat(string format, object arg0, object arg1, object arg2)
		{
			if (_logger.IsInfoEnabled)
			{
				_logger.InfoFormat(format, arg0, arg1, arg2);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the Info level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">The first object to format</param>
		/// <param name="arg1">The second object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		/// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		public void InfoFormat(string format, object arg0, object arg1)
		{
			if (_logger.IsInfoEnabled)
			{
				_logger.InfoFormat(format, arg0, arg1);
			}
		}

		/// <summary>
		/// Logs a formatted message string with the Info level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="arg0">An Object to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		/// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		public void InfoFormat(string format, object arg0)
		{
			if (_logger.IsInfoEnabled)
			{
				_logger.InfoFormat(format, arg0);
			}
		}

		/// <overloads>Log a formatted string with the Info level.</overloads>
		/// <summary>
		/// Logs a formatted message string with the Info level.
		/// </summary>
		/// <param name="format">A String containing zero or more format items</param>
		/// <param name="args">An Object array containing zero or more objects to format</param>
		/// <remarks>
		/// <para>
		/// The message is formatted using the <c>String.Format</c> method. See
		/// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		/// of the formatting.
		/// </para>
		/// <para>
		/// This method does not take an <see cref="T:System.Exception"/> object to include in the
		/// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		/// methods instead.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		public void InfoFormat(string format, params object[] args)
		{
			if (_logger.IsInfoEnabled)
			{
				_logger.InfoFormat(format, args);
			}
		}

		#endregion // Info

		//#region // Fatal

		///// <summary>
		///// Log a message object with the Fatal level including
		///// the stack trace of the <see cref="T:System.Exception"/> passed
		///// as a parameter.
		///// </summary>
		///// <param name="message">The message object to log.</param>
		///// <param name="exception">The exception to log, including its stack trace.</param>
		///// <remarks>
		///// <para>
		///// See the <see cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object)"/> form for more detailed information.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object)"/>
		//public void Fatal(object message, System.Exception exception)
		//{
		//    if (_fatal.IsFatalEnabled)
		//    {
		//        _fatal.Fatal(message, exception);
		//    }
		//}

		///// <overloads>Log a message object with the Fatal level.</overloads>
		///// <summary>
		///// Log a message object with the Fatal level.
		///// </summary>
		///// <param name="message">The message object to log.</param>
		///// <remarks>
		///// <para>
		///// This method first checks if this logger is <c>FATAL</c>
		///// enabled. If this logger is enabled, then it converts the message object
		///// (passed as parameter) to a string by invoking the appropriate
		///// <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///// proceeds to call all the registered appenders in this logger 
		///// and also higher in the hierarchy depending on the value of 
		///// the additivity flag.
		///// </para>
		///// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///// to this method will print the name of the <see cref="T:System.Exception"/> 
		///// but no stack trace. To print a stack trace use the 
		///// <see cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object,System.Exception)"/> form instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object,System.Exception)"/>
		//public void Fatal(object message)
		//{
		//    if (_fatal.IsFatalEnabled)
		//    {
		//        _fatal.Fatal(message);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Fatal level.
		///// </summary>
		///// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="args">An Object array containing zero or more objects to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object)"/>
		//public void FatalFormat(IFormatProvider provider, string format, params object[] args)
		//{
		//    if (_fatal.IsFatalEnabled)
		//    {
		//        _fatal.FatalFormat(provider, format, args);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Fatal level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">The first object to format</param>
		///// <param name="arg1">The second object to format</param>
		///// <param name="arg2">The third object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object)"/>
		//public void FatalFormat(string format, object arg0, object arg1, object arg2)
		//{
		//    if (_fatal.IsFatalEnabled)
		//    {
		//        _fatal.FatalFormat(format, arg0, arg1, arg2);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Fatal level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">The first object to format</param>
		///// <param name="arg1">The second object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object)"/>
		//public void FatalFormat(string format, object arg0, object arg1)
		//{
		//    if (_fatal.IsFatalEnabled)
		//    {
		//        _fatal.FatalFormat(format, arg0, arg1);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Fatal level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">An Object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object)"/>
		//public void FatalFormat(string format, object arg0)
		//{
		//    if (_fatal.IsFatalEnabled)
		//    {
		//        _fatal.FatalFormat(format, arg0);
		//    }
		//}

		///// <overloads>Log a formatted string with the Fatal level.</overloads>
		///// <summary>
		///// Logs a formatted message string with the Fatal level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="args">An Object array containing zero or more objects to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Fatal(System.Object)"/>
		//public void FatalFormat(string format, params object[] args)
		//{
		//    if (_fatal.IsFatalEnabled)
		//    {
		//        _fatal.FatalFormat(format, args);
		//    }
		//}

		//#endregion // Fatal

		//#region // Info

		///// <summary>
		///// Log a message object with the Info level including
		///// the stack trace of the <see cref="T:System.Exception"/> passed
		///// as a parameter.
		///// </summary>
		///// <param name="message">The message object to log.</param>
		///// <param name="exception">The exception to log, including its stack trace.</param>
		///// <remarks>
		///// <para>
		///// See the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/> form for more detailed information.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		//public void Info(object message, System.Exception exception)
		//{
		//    if (_info.IsInfoEnabled)
		//    {
		//        _info.Info(message, exception);
		//    }
		//}

		///// <overloads>Log a message object with the Info level.</overloads>
		///// <summary>
		///// Log a message object with the Info level.
		///// </summary>
		///// <param name="message">The message object to log.</param>
		///// <remarks>
		///// <para>
		///// This method first checks if this logger is <c>INFO</c>
		///// enabled. If this logger is enabled, then it converts the message object
		///// (passed as parameter) to a string by invoking the appropriate
		///// <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///// proceeds to call all the registered appenders in this logger 
		///// and also higher in the hierarchy depending on the value of 
		///// the additivity flag.
		///// </para>
		///// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///// to this method will print the name of the <see cref="T:System.Exception"/> 
		///// but no stack trace. To print a stack trace use the 
		///// <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/> form instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		//public void Info(object message)
		//{
		//    if (_info.IsInfoEnabled)
		//    {
		//        _info.Info(message);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Info level.
		///// </summary>
		///// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="args">An Object array containing zero or more objects to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		//public void InfoFormat(IFormatProvider provider, string format, params object[] args)
		//{
		//    if (_info.IsInfoEnabled)
		//    {
		//        _info.InfoFormat(provider, format, args);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Info level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">The first object to format</param>
		///// <param name="arg1">The second object to format</param>
		///// <param name="arg2">The third object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		//public void InfoFormat(string format, object arg0, object arg1, object arg2)
		//{
		//    if (_info.IsInfoEnabled)
		//    {
		//        _info.InfoFormat(format, arg0, arg1, arg2);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Info level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">The first object to format</param>
		///// <param name="arg1">The second object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		//public void InfoFormat(string format, object arg0, object arg1)
		//{
		//    if (_info.IsInfoEnabled)
		//    {
		//        _info.InfoFormat(format, arg0, arg1);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Info level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">An Object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		//public void InfoFormat(string format, object arg0)
		//{
		//    if (_info.IsInfoEnabled)
		//    {
		//        _info.InfoFormat(format, arg0);
		//    }
		//}

		///// <overloads>Log a formatted string with the Info level.</overloads>
		///// <summary>
		///// Logs a formatted message string with the Info level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="args">An Object array containing zero or more objects to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Info(System.Object)"/>
		//public void InfoFormat(string format, params object[] args)
		//{
		//    if (_info.IsInfoEnabled)
		//    {
		//        _info.InfoFormat(format, args);
		//    }
		//}

		//#endregion // Info

		//#region // Warn

		///// <summary>
		///// Log a message object with the Warn level including
		///// the stack trace of the <see cref="T:System.Exception"/> passed
		///// as a parameter.
		///// </summary>
		///// <param name="message">The message object to log.</param>
		///// <param name="exception">The exception to log, including its stack trace.</param>
		///// <remarks>
		///// <para>
		///// See the <see cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object)"/> form for more detailed information.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object)"/>
		//public void Warn(object message, System.Exception exception)
		//{
		//    if (_warn.IsWarnEnabled)
		//    {
		//        _warn.Warn(message, exception);
		//    }
		//}

		///// <overloads>Log a message object with the Warn level.</overloads>
		///// <summary>
		///// Log a message object with the Warn level.
		///// </summary>
		///// <param name="message">The message object to log.</param>
		///// <remarks>
		///// <para>
		///// This method first checks if this logger is <c>WARN</c>
		///// enabled. If this logger is enabled, then it converts the message object
		///// (passed as parameter) to a string by invoking the appropriate
		///// <see cref="T:log4net.ObjectRenderer.IObjectRenderer"/>. It then 
		///// proceeds to call all the registered appenders in this logger 
		///// and also higher in the hierarchy depending on the value of 
		///// the additivity flag.
		///// </para>
		///// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception"/> 
		///// to this method will print the name of the <see cref="T:System.Exception"/> 
		///// but no stack trace. To print a stack trace use the 
		///// <see cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object,System.Exception)"/> form instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object,System.Exception)"/>
		//public void Warn(object message)
		//{
		//    if (_warn.IsWarnEnabled)
		//    {
		//        _warn.Warn(message);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Warn level.
		///// </summary>
		///// <param name="provider">An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information</param>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="args">An Object array containing zero or more objects to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object)"/>
		//public void WarnFormat(IFormatProvider provider, string format, params object[] args)
		//{
		//    if (_warn.IsWarnEnabled)
		//    {
		//        _warn.WarnFormat(provider, format, args);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Warn level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">The first object to format</param>
		///// <param name="arg1">The second object to format</param>
		///// <param name="arg2">The third object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object)"/>
		//public void WarnFormat(string format, object arg0, object arg1, object arg2)
		//{
		//    if (_warn.IsWarnEnabled)
		//    {
		//        _warn.WarnFormat(format, arg0, arg1, arg2);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Warn level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">The first object to format</param>
		///// <param name="arg1">The second object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object)"/>
		//public void WarnFormat(string format, object arg0, object arg1)
		//{
		//    if (_warn.IsWarnEnabled)
		//    {
		//        _warn.WarnFormat(format, arg0, arg1);
		//    }
		//}

		///// <summary>
		///// Logs a formatted message string with the Warn level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="arg0">An Object to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object)"/>
		//public void WarnFormat(string format, object arg0)
		//{
		//    if (_warn.IsWarnEnabled)
		//    {
		//        _warn.WarnFormat(format, arg0);
		//    }
		//}

		///// <overloads>Log a formatted string with the Warn level.</overloads>
		///// <summary>
		///// Logs a formatted message string with the Warn level.
		///// </summary>
		///// <param name="format">A String containing zero or more format items</param>
		///// <param name="args">An Object array containing zero or more objects to format</param>
		///// <remarks>
		///// <para>
		///// The message is formatted using the <c>String.Format</c> method. See
		///// <see cref="M:System.String.Format(System.String,System.Object[])"/> for details of the syntax of the format string and the behavior
		///// of the formatting.
		///// </para>
		///// <para>
		///// This method does not take an <see cref="T:System.Exception"/> object to include in the
		///// log event. To pass an <see cref="T:System.Exception"/> use one of the <see cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object,System.Exception)"/>
		///// methods instead.
		///// </para>
		///// </remarks>
		///// <seealso cref="M:VKeCRM.Common.Logging.Logger.Warn(System.Object)"/>
		//public void WarnFormat(string format, params object[] args)
		//{
		//    if (_warn.IsWarnEnabled)
		//    {
		//        _warn.WarnFormat(format, args);
		//    }
		//}

		//#endregion // Warn
    }
}
