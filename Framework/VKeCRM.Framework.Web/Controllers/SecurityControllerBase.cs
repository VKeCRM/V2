using System;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;

using VKeCRM.Framework.Web.Exceptions;
//using VKeCRM.Framework.Web.Shared.UI;
using VKeCRM.Framework.Web.State;
using VKeCRM.Framework.Web.Security;
//using VKeCRM.InternalDataProvider.Controllers;
//using VKeCRM.Security.DataTransferObjects;
using VKeCRM.Common.Utility;
using VKeCRM.Framework.Business.Enums;
using VKeCRM.Common.Logging;
//using VKeCRM.Common.ApplicationSetting;

namespace VKeCRM.Framework.Web.Controllers
{
	public abstract class SecurityControllerBase : ControllerBase
	{
		#region Declarations

		private static string _logoutUrl = "signout.aspx"; //default value for logout page

		/// <summary>
		/// Defines the QueryString keys that are used specifically for Security functionality
		/// </summary>
		protected struct QueryStringKey
		{
			public const string ReturnUrl = "ReturnUrl"; // just needed for asp.net login functionality
			public const string ReturnUrlSslRequired = "s"; // will indicate whether the return url was using ssl
			public const string ReasonCode = "ReasonCode"; // if user's credentials are not valid, direct them to the signin page with a ReasonCode
		}

		#endregion


		#region Methods

		/// <summary>
		/// Sets the principal from the session. If there is no principal in session and 
		/// the AuthCookie is used to create a VKeCRMPrincipal and load a VKeCRMIdentity.
		/// </summary>
		/// <remarks>When loading from AuthCookie, the principal is not fully authenticated.</remarks>
		public void SetCurrentPrincipalFromSession()
		{
			if ((!(HttpContext.Current.User is VKeCRMPrincipal)) &&
				(HttpContext.Current.Request.IsAuthenticated))
			{
				// Try to retrieve from session first
				VKeCRMPrincipal principal = SessionState.CurrentPrincipal;

				// If principal in session, then set the current context
				//if (principal != null && HttpContext.Current.User.Identity is VKeCRMIdentity<Member>)
				if (principal != null && principal.Identity is VKeCRMIdentity)
				{
					HttpContext.Current.User = principal;
				}
				else
				{
					//Member member = null;
					try
					{
						//load user from application specific implementation of getting the Member object.
						//member = GetMemberByUserName(HttpContext.Current.User.Identity.Name);
					}
					catch (Exception ex)
					{
						LoggerManager.MemberLogger.Debug(ex);
						FormsAuthentication.SignOut();
					}

					//PZ: we need to retain the full authentication flag since the Member may be
					//transferred between different site and the whole SessionState.CurrentPrincipal
					//will be re-constructed.
					bool isMemberAuthenticated = false;

					//PZ: 011810 the KickedOut flag has to be retained as well when you swtiching between sites
					bool isKickedOut = SessionState.CurrentPrincipal == null ? false : SessionState.CurrentPrincipal.IsKickedOut;

					if (SessionState.CurrentPrincipal != null &&
							SessionState.CurrentPrincipal.IsFullyAuthenticated)
					{
						isMemberAuthenticated = true;
					}

					//SetCurrentPrincipal(LoadIdentity(member));
					SessionState.CurrentPrincipal.IsFullyAuthenticated = isMemberAuthenticated;
					SessionState.CurrentPrincipal.IsKickedOut = isKickedOut;

					// Check for empty identity
					if (HttpContext.Current.User.Identity.Name.Equals(string.Empty))
					{
						// Unable to retrieve the principal based on the cookie, so kill the cookie
						FormsAuthentication.SignOut();

						// Force a new session
						HttpContext.Current.Session.Abandon();
					}
				}
			}
			else if (HttpContext.Current.Request.IsAuthenticated)
			{   // This case shouldn't occur, because we're using forms authentication,
				// but just to be safe, use the VKeCRMPrincipal in the session. 

				// Try to retrieve the pricipal
				VKeCRMPrincipal principal = SessionState.CurrentPrincipal;
				if (principal != null)
				{
					// Set the context to the principal in the session.
					HttpContext.Current.User = SessionState.CurrentPrincipal;
				}
				else
				{
					// Unable to retrieve the principal, so kill the cookie
					FormsAuthentication.SignOut();

					// Force a new session
					HttpContext.Current.Session.Abandon();
				}
			}
		}

		/// <summary>
		/// The inheriting class must implement this to get the application specific member.
		/// </summary>
		/// <param name="userName">The name of the user to retrieve.</param>
		/// <returns>Application specific user.</returns>
        //protected virtual Member GetMemberByUserName(string userName) 
        //{
        //    return MemberController.Instance.GetMemberByUserName(userName);
        //}

        //public virtual bool Exists(int memberId)
        //{
        //    return (null != MemberController.Instance.GetMemberById(memberId));
        //}

		/// <summary>
		/// Creates Principal and Identity
		/// </summary>
		public void SetCurrentPrincipal(IIdentity identity)
		{
			// Create Principal and Identity set session value
			SessionState.CurrentPrincipal = VKeCRMPrincipal.GetPrincipalFromCookie(identity) as VKeCRMPrincipal;
		}

		/// <summary>
		/// Creates a VKeCRMIdentity and loads the member that is passed in.
		/// </summary>
		/// <param name="member">The unique username of the member to load.</param>
		/// <returns>The new VKeCRMIdentity</returns>
        //public VKeCRMIdentity LoadIdentity(Member member)
        //{
        //    // Create VKeCRMIdentity using loaded member
        //    VKeCRMIdentity identity = new VKeCRMIdentity(member);
        //    return identity;
        //}

		/// <summary>
		/// Gets the login URL used for this application
		/// </summary>
		public static string GetLoginUrl()
		{
			return string.Format("https://{0}{1}?{2}={3}&{4}={5}",
					HttpContext.Current.Request.Url.Host.ToString(),
					FormsAuthentication.LoginUrl,
					QueryStringKey.ReturnUrl,
					HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery),
					QueryStringKey.ReturnUrlSslRequired,
					HttpContext.Current.Request.IsSecureConnection.ToString());
		}

		/// <summary>
		/// Gets the full-qualified login URL used for this application
		/// </summary>
		public static string GetFullLoginUrl()
		{
			return string.Format("https://{0}{1}?{2}={3}&{4}={5}&{6}={7}",
					HttpContext.Current.Request.Url.Host.ToString(),
					FormsAuthentication.LoginUrl,
					QueryStringKey.ReturnUrl,
					HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery),
					QueryStringKey.ReturnUrlSslRequired,
					HttpContext.Current.Request.IsSecureConnection.ToString(),
					QueryStringKey.ReasonCode, "securearea");
		}

        //public static string GetZapLoginUrl()
        //{
        //    //return string.Format("https://{0}/plugin/signin.aspx?{1}={2}&{3}={4}",
        //    //    ApplicationSettingsManager.Instance.GlobalApplicationSettings.PortalHostName,
        //    //    QueryStringVariable.Side,
        //    //    HttpContext.Current.Request[QueryStringVariable.Side],
        //    //    QueryStringVariable.Symbol,
        //    //    HttpContext.Current.Request[QueryStringVariable.Symbol]
        //    //);

        //    return string.Format("https://{0}/plugin/signin.aspx?{1}",
        //                         ApplicationSettingsManager.Instance.GlobalApplicationSettings.PortalHostName,
        //                         HttpContext.Current.Request.QueryString);
        //}

		/// <summary>
		/// Gets the logout URL used for this application
		/// </summary>
		public static string GetLogoutUrl()
		{
			return _logoutUrl;
		}

		/// <summary>
		/// Gets the fully-qualified return URL that was passed to the login page
		/// </summary>
		public static string GetReturnUrl()
		{
			if (HttpContext.Current.Request.QueryString[QueryStringKey.ReturnUrl] != null)
			{
				bool useSsl = System.Convert.ToBoolean(HttpContext.Current.Request.QueryString[QueryStringKey.ReturnUrlSslRequired]);
				string protocol = !useSsl ? "http" : "https";
				string returnUrl = HttpContext.Current.Request.QueryString[QueryStringKey.ReturnUrl];

				// Hacked by jian.li in 10/28/2008
				// TODO: Maybe, removing the excrescent symbol parameter in the return url 
				// can be implemented in another natural local.
				// Remove the excrescent symbol parameter in the return url. 
				// Avoid the return url look as: quotess/AAPL/default.aspx?symbol=AAPL
				string symbolPattern = @"[\?|&]symbol=\S+";
				returnUrl = Regex.Replace(returnUrl, symbolPattern, string.Empty);

				if (!returnUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
				{
					returnUrl = string.Format("{0}://{1}{2}", protocol, HttpContext.Current.Request.Url.Host.ToString(), returnUrl);
				}

				return returnUrl;
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		///		Sets the current principal for the member that is passed in
		/// </summary>
		/// <param name="member">
		///		Set the current signing in member.
		/// </param>
		/// <param name="createPersistentCookie">
		///		Set a value which indicates whether save the member information into cookie.
		/// </param>
        //public virtual void SignInMember(Member member, bool createPersistentCookie)
        //{
        //    SignInMember(member, createPersistentCookie, true);
        //}

        //public virtual void SignInMember(Member member, bool createPersistentCookie, bool isWeb) 
        //{
        //    SignInMember(member, createPersistentCookie, isWeb, true);
        //}

        //public virtual void SignInMember(Member member, bool createPersistentCookie, bool isWeb,bool needLeadTracking)
        //{
        //    // Set the Auth Cookie
        //    FormsAuthentication.SetAuthCookie(member.UserName, createPersistentCookie);

        //    SetCurrentPrincipal(LoadIdentity(member));

        //    ///this step should only be call in web site
        //    if (isWeb)
        //    {
        //        // Mark as fully authenticated
        //        SessionState.CurrentPrincipal.IsFullyAuthenticated = true;
        //        // Mark Cookies signin
        //        ShareUserInfo.Instance.SetUserInfo(member);
				
        //        // Lead Tracking
        //        if (needLeadTracking)
        //        {
        //            // TODO: LeadTracking
        //        }
        //    }

        //    VKeCRM.Framework.Web.State.SessionState.EnableRoadblock = true;

        //    // save login info for #11872 , chao 20100505   
        //    SessionState.Session["LoginIP"] = this.Request.UserHostAddress;
        //    SessionState.Session["LoginUrlReferrer"] = this.Request.UrlReferrer;
        //    SessionState.Session["LoginUserAgent"] = this.Request.UserAgent;
        //    SessionState.Session["LoginUserHostName"] = this.Request.UserHostName;
        //    SessionState.Session["LoginUrl"] = this.Request.Url;
        //    SessionState.Session["LoginQueryString"] = this.Request.QueryString;
        //    SessionState.Session["LoginDateTime"] = DateTime.UtcNow;
        //}

		/// <summary>
		/// Validates credentials and sets the security context of the user.
		/// </summary>
		/// <param name="userName">User name entered by User to sign in</param>
		/// <param name="password">User's password for signing in</param>
		/// <param name="createPersistentCookie">A boolean value to either create a persisten cookie or not</param>
		/// <return>Whether the username and password combination is valid</return>
		public bool SignIn(string userName, string password, bool createPersistentCookie)
		{
			bool result = false;
			try
			{
				// this needs to be wrapped in exception handling because we are using a WCF wrapper
				result = Membership.ValidateUser(userName, password);
			}
			catch (System.ServiceModel.FaultException ex)
			{
				throw GetWebExceptionFromServiceFault(ex);
			}

			// for now we are erring on the side of security and not allowing information about
			// whether a _MemberoDisplay exists or not to bubble to the top. if the _MemberoDisplay doesn't exist yet
			// the user will just get a generic error message about not being able to log in.
			if (result)
			{
                //Member member = GetMemberByUserName(userName);

                //SignInMember(member, createPersistentCookie);
			}

			return result;
		}

		/// <summary>
		/// Signs the currently logged in user out.
		/// </summary>
		/// <remarks>User is not redirected after sign out.</remarks>
		/// <see cref="SignOut(string)"/>
		public static void SignOut()
		{
			// Sign the user out
			SignOut(null);
		}

		/// <summary>
		/// Signs the currently logged in user out.
		/// </summary>
		/// <param name="redirectToUrl">URL to redirect to after signing out.</param>
		public static void SignOut(string redirectToUrl)
		{

			// Clear the session bag
			HttpContext.Current.Session.Clear();

			// Abandon the session altogether
			HttpContext.Current.Session.Abandon();

			/// set HttpContext.Current.Request.IsAuthenticated = false
			FormsAuthentication.SignOut();

			// clear user info in Cookies 
			//ShareUserInfo.Instance.ClearUserInfo();

			// An empty or null redirection url indicates no redirection is needed
			if (!string.IsNullOrEmpty(redirectToUrl))
			{
				// Redirect to the desired url
				HttpContext.Current.Response.Redirect(redirectToUrl, true);
			}

			//clear the cookie for SSO token
			//SsoToken.Clear();
		}

		/// <summary>
		/// Returns whether the current member logged into this session through the login form and NOT through a cookie.
		/// If the member logged in through the cookie, this should be false.
		/// </summary>
		/// <returns></returns>
		public static bool IsCurrentMemberFullyAuthenticated()
		{
			return (IsCurrentMemberAuthenticated() &&
				(SessionState.CurrentPrincipal != null) && (SessionState.CurrentPrincipal.IsFullyAuthenticated));
		}

		/// <summary>
		/// Returns whether the current member logged in through the cookie OR through the sign in form
		/// </summary>
		/// <returns></returns>
		public static bool IsCurrentMemberAuthenticated()
		{
			return HttpContext.Current.Request.IsAuthenticated;
		}

		/// <summary>
		/// Returns whether the current member logged in through the cookie OR through the sign in form
		/// </summary>
		/// <returns></returns>
        //public static bool IsCurrentMemberIPAuthenticated()
        //{
        //    //check whether HttpContext.Current.Request.UserHostAddress is in list
        //    ApiIpWhiteList apiIpWhiteList =
        //        ApiIpWhiteListController.Instance.GetApiIpWhiteListByIpAddress(HttpContext.Current.Request.UserHostAddress);
        //    return apiIpWhiteList == null || apiIpWhiteList.IsActive == false ? false : true;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string DoKickedOutSession() 
        {
            string reasonCode = string.Empty;
            if (SecurityControllerBase.IsCurrentMemberFullyAuthenticated() && SessionState.CurrentPrincipal != null && SessionState.CurrentPrincipal.IsKickedOut) 
            {
                // set the value before signout
                reasonCode = "doublelogin";
                if (!string.IsNullOrEmpty(SessionState.CurrentPrincipal.KickReasonCode))
                {
                    reasonCode = SessionState.CurrentPrincipal.KickReasonCode;
                }

                // add log for #11872 , chao 20100330   HttpContext.Current.User.Identity.Name
                LoggerManager.KickOutInfoLogger.Info(string.Format(@"LaterSession: CurrentSessionId = {0}; UserName = {1};OperatingSystem = {2};
                                    Url = {3};UrlReferrer = {4};CurrentUserHostAddress = {5} ; KickOutReasonCode = {6};"
                    , HttpContext.Current.Session.SessionID, HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserAgent
                    , HttpContext.Current.Request.Url, HttpContext.Current.Request.UrlReferrer, HttpContext.Current.Request.UserHostAddress, reasonCode));

                SecurityControllerBase.SignOut();
            }
            return reasonCode;
        }
        
        /// <summary>
        ///     Post email address to eloqua while user signup or change email address
        /// </summary>
        /// <param name="elqCustomerGuid">
        ///     Set the customer guid id of eloqua
        /// </param>
        /// <param name="emailAddress">
        ///     Set the new email address
        /// </param>
        //public void PostToEloqua(string elqCustomerGuid, string emailAddress)
        //{
        //    // Proof of concept for posting Eloqua pixel data to Eloqua.  Once worked, the following code needs to be refined.
        //    string eloquaUrl = ApplicationSettingsManager.Instance.GlobalApplicationSettings.EloquaUrl;
        //    string elqCookieWrite = ApplicationSettingsManager.Instance.GlobalApplicationSettings.EloquaCookieWrite;
        //    string elqFormName = ApplicationSettingsManager.Instance.GlobalApplicationSettings.EloquaFormName;
        //    string elqSiteId = ApplicationSettingsManager.Instance.GlobalApplicationSettings.EloquaSiteId;

        //    if (!string.IsNullOrEmpty(elqCustomerGuid))
        //    {
        //        // get post content
        //        string postData =
        //            string.Format(
        //                "elqCookieWrite={0}&elqFormName={1}&elqSiteId={2}&elqCustomerGuid={3}&EmailAddress={4}",
        //                elqCookieWrite, elqFormName, elqSiteId, elqCustomerGuid, emailAddress);

        //        // use web request to post to Eloqua
        //        WebRequest request = WebRequest.Create(eloquaUrl);
        //        request.Method = "POST";
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.ContentLength = postData.Length;
        //        using (Stream writeStream = request.GetRequestStream())
        //        {
        //            byte[] postDataRaw = Encoding.Default.GetBytes(postData);
        //            writeStream.Write(postDataRaw, 0, postDataRaw.Length);
        //        }

        //        // get response result back from postint to Eloqua
        //        string result = string.Empty;
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (Stream responseStream = response.GetResponseStream())
        //            {
        //                using (StreamReader readStream = new StreamReader(responseStream, Encoding.Default))
        //                {
        //                    result = readStream.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}
		#endregion
	}
}