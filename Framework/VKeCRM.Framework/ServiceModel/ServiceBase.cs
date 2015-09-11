//-----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;


namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// A ServiceBase that implements common behaviors of all the Services, including error logging.
	/// </summary>
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode=AddressFilterMode.Any)]
	[ErrorHandlerServiceBehavior]
	public class ServiceBase
	{
		#region Private fields
		/// <summary>
		/// Indicates that we have an error occurred during the service call.
		/// </summary>
		protected bool hasError;

        /// <summary>
        /// If set it is true,enforce using cache not notice of wcf client setting
        /// </summary>
	    protected bool enforceSecondCache = false;
		/// <summary>
		/// The exception thrown from the business logic.
		/// </summary>
		protected Exception unknownError;
		#endregion

		#region Properties

		/// <summary>
		/// Indicates that we have an error occurred during the service call.
		/// </summary>
		internal bool HasError
		{
			get { return hasError; }
			set { hasError = value; }
		}

		/// <summary>
		/// This is the unexpected error, mostly data access exception, occurred during AfterInvoke.
		/// </summary>
		internal Exception UnknownError
		{
			get { return unknownError; }
			set { unknownError = value; }
		}
		#endregion

		/// <summary>
		/// Centralized place to log all the exception occurred during the service call.
		/// </summary>
		protected internal void LogException(Exception ex)
		{
			//TODO implement the centralized exception logging here

		}
	}
}
