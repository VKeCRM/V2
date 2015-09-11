using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VKeCRM.Framework.Web.UI.Controls;
using VKeCRM.Framework.Web.Controllers;


namespace VKeCRM.Framework.Web.Mvc.Controller
{
	public class JsonControllerBase : VKeCRM.Framework.Mvc.ControllerBase
	{
		public override bool IsCookieAuthentication()
		{
			return SecurityControllerBase.IsCurrentMemberAuthenticated();
		}

		public override bool IsFullAuthentication()
		{
			return SecurityControllerBase.IsCurrentMemberFullyAuthenticated();
		}

        public override bool IsIPAuthentication()
        {
            //return SecurityControllerBase.IsCurrentMemberIPAuthenticated();
            return true;
        }

		public override string GetFullLoginUrl()
		{
			return SecurityControllerBase.GetFullLoginUrl();
		}

		public override string GetKickOutInfo()
		{
			return SecurityControllerBase.DoKickedOutSession();
		}
	}
}
