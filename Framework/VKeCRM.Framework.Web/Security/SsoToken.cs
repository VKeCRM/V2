using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VKeCRM.Framework.Web.Security
{
	/// <summary>
	/// Provide functionalities to encrypt/decrypt SSO tokens that being shared by Portal and Trading
	/// </summary>
	public sealed class SsoToken
	{
		private const string CookieName = "ssotoken";


		#region Properties
		/// <summary>
		/// User name storeed this SSO token
		/// </summary>
		public string UserName
		{
			get;
			set;
		}

		/// <summary>
		/// When this SSO token will expire. 
		/// </summary>
		public DateTime Expires
		{
			get;
			set;
		}

		/// <summary>
		/// Indicates whether the owner of the token is fully authenticated.
		/// </summary>
		public bool IsFullyAuthenticated
		{
			get;
			private set;
		}

		
		#endregion

		/// <summary>
		/// Persist the token into a cookie
		/// </summary>
		public void Persist()
		{
			string s = string.Concat(UserName, "|", Expires.Ticks);
			//TODO encrypt the token
			HttpCookie cookie = new HttpCookie(CookieName, s);
			cookie.Domain = "VKeCRM.com";
			
			//TODO if we persist this sso cookie as persistent cookie, we have to implement "window counting" to expires this when all the windows are closed
			//before we can do that, setting as a persistent cookie will have users to full fully authenticated (before session expires) even if the close the window
			//so save the cookie as in-memory for now, the draw back is if user opens another IE window, they will not be authenticated through SSO.
			//cookie.Expires = Expires;
			HttpContext.Current.Response.SetCookie(cookie);
		}

		public static SsoToken Current
		{
			get
			{
				SsoToken token = new SsoToken();
				token.IsFullyAuthenticated = false;


				HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(CookieName);
				if (cookie != null)
				{
					string s = cookie.Value;
					//TODO decrypt the  string and return an SSO token
					string[] vals = s.Split('|');
					if (vals.Length != 2)
					{
						//something is wrong, this can not be a valid sso token.
						//TODO: log this
					}

					token.UserName = vals[0];
					token.Expires = new DateTime(Convert.ToInt64(vals[1]));
					token.IsFullyAuthenticated = (token.Expires > DateTime.Now);
				}

				return token;
			}
		}

		/// <summary>
		/// Clear the SSO token.
		/// </summary>
		public static void Clear()
		{
			HttpContext.Current.Response.Cookies.Remove(CookieName);
		}
	}
}
