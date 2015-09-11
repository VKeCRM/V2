using System.Web;
using System.Web.SessionState;

//using VKeCRM.Common.LookupEnums.Apply.Enums;
using VKeCRM.Framework.Web.Security;

namespace VKeCRM.Framework.Web.State
{
    /// <summary>
    /// Use this class to manage access to data stored in the session bag.
    /// </summary>
    public class SessionState
    {

		#region Constructors

        /// <summary>
        /// No need to construct, all properties should be public, static.
        /// </summary>
        protected SessionState()
        {
		}

		#endregion

        /// <summary>
        /// Returns a reference to the current Session object. Should not really be used unless 
        /// absolutely necessary.
        /// </summary>
        public static HttpSessionState Session
        {
            get
            {
                return System.Web.HttpContext.Current.Session;
            }
        }

        #region Security Related

        /// <summary>
        /// Returns the current principal of the logged in user as a VKeCRMPrincipal
        /// </summary>
        public static VKeCRMPrincipal CurrentPrincipal
        {
            get
            {
                return GetSessionValue("principal", null) as VKeCRMPrincipal;
            }
            set
            {
                SetSessionValue("principal", value);
            }
        }

        ///// <summary>
        ///// Returns the Member DTO of the currently logged in user as a DataTransferObjects.Member.
        ///// </summary>
        //public static Member CurrentMember
        //{
        //    get
        //    {
        //        VKeCRMPrincipal principal = CurrentPrincipal;
        //        VKeCRMIdentity identity = ((principal != null) ? principal.Identity : null) as VKeCRMIdentity;
        //        return (identity != null) ? identity.User : null;
        //    }
        //}

		public static bool EnableRoadblock
		{
			get
			{
				return (bool)GetSessionValue("enableroadblock", false);
			}
			set
			{
				SetSessionValue("enableroadblock", value);
			}
		}

    	public static bool IsContentTest
    	{
    		get{return (bool) GetSessionValue("iscontenttest", false);}
			set{SetSessionValue("iscontenttest", value);}
    	}

        #endregion // Security Related

        //#region Internal Sessions for IAV
        //public static string AchBankName
        //{
        //    get
        //    {
        //        return GetSessionValue("AchBankName", null) as string;
        //    }
        //    set
        //    {
        //        SetSessionValue("AchBankName", value);
        //    }
        //}

        //public static long TransferAccountId
        //{
        //    get
        //    {
        //        return (long)GetSessionValue("TransferAccountId", 0);
        //    }
        //    set
        //    {
        //        SetSessionValue("TransferAccountId", value);
        //    }
        //}

        //public static UserContext CurrentUserContext
        //{
        //    get
        //    {
        //        return GetSessionValue("CurrentUserContext", null) as UserContext;
        //    }
        //    set
        //    {
        //        SetSessionValue("CurrentUserContext", value);
        //    }
        //}

        //public static MfaRefreshInfo MfaRefreshInfo
        //{
        //    get
        //    {
        //        return GetSessionValue("MfaRefreshInfo", null) as MfaRefreshInfo;
        //    }
        //    set
        //    {
        //        SetSessionValue("MfaRefreshInfo", value);
        //    }
        //}

        //public static IAVStatusType? IavStatus
        //{
        //    get
        //    {
        //        return (IAVStatusType?)GetSessionValue("IavStatus", null);
        //    }
        //    set
        //    {
        //        SetSessionValue("IavStatus", value);
        //    }
        //}

        //public static BankAccount YodleeBankAccount
        //{
        //    get
        //    {
        //        return GetSessionValue("YodleeBankAccount", null) as BankAccount;
        //    }
        //    set
        //    {
        //        SetSessionValue("YodleeBankAccount", value);
        //    }
        //}

        //public static VerifiableAccount VerifiableAccount
        //{
        //    get
        //    {
        //        return GetSessionValue("VerifiableAccount", null) as VerifiableAccount;
        //    }
        //    set
        //    {
        //        SetSessionValue("VerifiableAccount", value);
        //    }
        //}

        //public static long ItemId
        //{
        //    get
        //    {
        //        return (long)GetSessionValue("ItemId", 0);
        //    }
        //    set
        //    {
        //        SetSessionValue("ItemId", value);
        //    }
        //}

        //public static byte[] IavCaptchaImage
        //{
        //    get
        //    {
        //        return GetSessionValue("IavCaptchaImage", null) as byte[];
        //    }
        //    set
        //    {
        //        SetSessionValue("IavCaptchaImage", value);
        //    }
        //}
        //#endregion

        #region Inter-Page Communication

        /// <summary>
        /// Message to display at the top of a page when redirected from one page to the next. For example,
        /// when the user changes their password, the user is redirected back to the edit profile page with
        /// a message on top that says the password was changed.
        /// </summary>
        public static string PageMessage
        {
            get
            {
                // Retrieve the message
                string result = (string) GetSessionValue("PageMessage", string.Empty);
                
                // Clear the message in the bag
                PageMessage = null;

                // return the message
                return result;
            }
            set
            {
                // We always store empty strings when null
                SetSessionValue("PageMessage", value ?? string.Empty);
            }
        }

        #endregion //Inter-Page Communication

        #region Internal Helper Methods

        /// <summary>
        /// Used by this class and application state classes to get a session value.
        /// </summary>
        /// <param name="name">The name of the item in the session bag.</param>
        /// <param name="defaultValue">The default value to return if the item is not present in the session bag.</param>
        /// <returns>The item retrieved from the session bag or the default value if not found.</returns>
        protected static object GetSessionValue(string name, object defaultValue)
        {
            object result = null;

            try
            {
                if (null != HttpContext.Current.Session)
                {
                    result = HttpContext.Current.Session[name];
                }
                else
                {
					// TODO: Implement using new Logging Framework
					//ControllerBase.Logging.CommonLogger.WarnFormat(string.Format("Session State: Unable to read '{0}'", name));
                }
            }
            catch (System.Exception ex)
            {
				// TODO: Implement using new Logging Framework
				//ControllerBase.Logging.CommonLogger.WarnFormat(string.Format("Session State: Unable to read '{0}'", name), ex);
            }

            return (result ?? defaultValue);
        }

        /// <summary>
        /// Used by this class and application state classes to set a session value.
        /// </summary>
        /// <param name="name">The name of the item to store in the session bag.</param>
        /// <param name="newValue">The value of the item to store in the session bag.</param>
        protected static void SetSessionValue(string name, object newValue)
        {
            try
            {
                HttpContext.Current.Session[name] = newValue;
            }
            catch (System.Exception ex)
            {
				// TODO: Implement using new Logging Framework
				//ControllerBase.Logging.CommonLogger.WarnFormat(string.Format("Session State: Unable to save '{0}'='{1}'", name, newValue), ex);
            }
        }

        #endregion // Internal Helper Methods
    }
}
