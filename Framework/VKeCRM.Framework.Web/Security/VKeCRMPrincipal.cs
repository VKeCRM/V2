using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace VKeCRM.Framework.Web.Security
{
	/// <summary>
	/// VKeCRM custom principal object.
	/// </summary>
	[Serializable]
	public class VKeCRMPrincipal : IPrincipal
	{
		#region Declarations
		
		private IIdentity _identity;
		private string[] _roles;

	    #endregion

		#region Constructors
		
		public VKeCRMPrincipal(IIdentity identity, string[] roles)
		{
            _identity = identity;
			_roles = roles;
			IsKickedOut = false;
		}
		#endregion

		#region IPrincipal Members

		public IIdentity Identity
		{
			get { return _identity; }
		}

		public bool IsInRole(string role)
		{
			if (_roles != null)
			{
				for (int i = 0; i < _roles.Length; i++)
				{
					if (_roles[i].Equals(role, StringComparison.CurrentCultureIgnoreCase))
					{
						return true;
					}
				}
			}

			return false;
		}

	    public bool IsFullyAuthenticated { get; set; }


		public bool IsKickedOut { get; set; }

        public string KickReasonCode { get; set; }
	    #endregion

		#region Static Methods


		/// <summary>
		/// Creates Principal and Identity based on the user name and roles from the 
		/// asp.net authentication cookie;
		/// </summary>
		/// <returns>The current principal</returns>
        public static IPrincipal GetPrincipalFromCookie(IIdentity identity)
		{
			string cookieName = FormsAuthentication.FormsCookieName;
			HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];

			if (authCookie == null)
			{
				// There is no authentication cookie.
				return SetEmptyPrincipal();
			}

			FormsAuthenticationTicket authTicket = null;
			try
			{
				authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			}
			catch
			{
				// error occured. Let user authenticate again
				return SetEmptyPrincipal();
			}

			if (authTicket == null)
			{
				// Cookie failed to decrypt.
				return SetEmptyPrincipal();
			}

            // Whenever we load cookie we always load the Roles instead of loading it from Userdata
            string[] roles = Roles.GetRolesForUser(identity.Name);

			IPrincipal principal = new VKeCRMPrincipal(identity, roles);
			HttpContext.Current.User = principal;
			return principal;
		}

		/// <summary>
		/// Sets the HttpContext.Current.User to an empty principal
		/// </summary>
		/// <returns>The empty principal</returns>
		private static IPrincipal SetEmptyPrincipal()
		{
			IIdentity identity = new VKeCRMIdentity();
            IPrincipal principal = new VKeCRMPrincipal(identity, new string[0]);
            HttpContext.Current.User = principal;
			return principal;
		}


		#endregion
	}
}
