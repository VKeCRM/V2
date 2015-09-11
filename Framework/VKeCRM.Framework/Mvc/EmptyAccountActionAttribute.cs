using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
    public class EmptyAccountActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if account is Empty
			if ((bool)MySession["TradingAccountIsEmpty"] && !filterContext.ActionDescriptor.ActionName.Equals("ChangeAccountNumber", StringComparison.OrdinalIgnoreCase))
			{
                throw new Mvc.Exceptions.MvcEmptyAccountException();
            }
            
        }

        public virtual System.Web.SessionState.HttpSessionState MySession
        {
            get
            {
                return System.Web.HttpContext.Current.Session;
            }
        }
    }
}
