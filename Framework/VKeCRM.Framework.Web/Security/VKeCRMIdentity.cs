using System;
using System.Security.Principal;
//using VKeCRM.Security.DataTransferObjects;

namespace VKeCRM.Framework.Web.Security
{
	/// <summary>
	/// VKeCRM custom identity object
	/// </summary>
	[Serializable]
	public class VKeCRMIdentity : IIdentity
	{
		#region Fields
		
		private readonly string _authenticationType = "VKeCRM";
		//private Member _user;
		
		#endregion

		#region Constructors
		/// <summary>
		/// Creates empty VKeCRMIdentity object
		/// </summary>
		public VKeCRMIdentity()
		{
		}

        //public VKeCRMIdentity(Member user)
        //{ 
        //    _user = user;
        //}

		#endregion

        //public Member User
        //{
        //    get { return _user; }
        //}

		#region IIdentity Members
		///<summary>
		///Gets the type of authentication used.
		///</summary>
		public string AuthenticationType
		{
			get { return _authenticationType; }
		}

		///<summary>
		///Gets a value that indicates whether the user has been authenticated.
		///</summary>
        public bool IsAuthenticated
        {
            //get { return (_user != null); }
            get { return true; }
        }

		/// <summary>
		/// Get the name of the current user.
		/// </summary>
        public string Name
        {
            //get { return _user != null ? _user.UserName : string.Empty; }
            get { return string.Empty; }
        }

		#endregion

	}
}
