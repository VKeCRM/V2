using System;
using System.Net;
using System.ServiceModel;
using VKeCRM.Common;
using VKeCRM.Common.Utility;
using VKeCRM.Framework.Business.Enums;
using VKeCRM.Framework.Mvc;
//using VKeCRM.Portal.Web.Framework.UI;
using VKeCRM.Framework.Web.Controllers;
using VKeCRM.Framework.Web.Exceptions;
using VKeCRM.Framework.Web.Mvc.Controller;
using VKeCRM.Portal.Web.Framework.Logging;

namespace VKeCRM.Portal.Web.Framework.JsonControllers
{
    public class PortalJsonControllerBase : JsonControllerBase
    {


        public override System.Web.Mvc.ActionResult HandleError(Exception ex)
        {
            // used for logging here
            if (!(ex is VKeCRM.Framework.Mvc.Exceptions.MvcExceptionBase))
            {
                PortalLoggerManager.PortalWebLogger.ErrorFormat("Error In MVC Controller: {0}", ex.ToString());
            }

            return base.HandleError(ex);
        }

        public override VKeCRM.Framework.Mvc.AuthenticationLevel RequireAuthenticationLevel
        {
            get
            {
                return AuthenticationLevel.None;
            }
        }

        //TODO  ayang 0604- we ignore authentication for now
        public override bool IsCookieAuthentication()
        {
            return SecurityControllerBase.IsCurrentMemberAuthenticated();
        }

        //TODO  ayang 0604- we ignore authentication for now
        public override bool IsFullAuthentication()
        {
            return SecurityControllerBase.IsCurrentMemberFullyAuthenticated();
        }

        #region Error Handling
        /// <summary>
        /// This method translates an exception from the service layer to an exception for the presentation layer
        /// </summary>
        /// <param name="faultException">The fault exception from the service layer</param>
        /// <returns>Returns the exception for the service fault</returns>            
        protected WebExceptionBase GetWebExceptionFromServiceFault(System.ServiceModel.FaultException faultException)
        {
            int errorCodeInInt;
            WebExceptionBase webException = null;

            if (int.TryParse(faultException.Code.Name, out errorCodeInInt))
            {
                ExceptionErrorCode errorCode = (ExceptionErrorCode)errorCodeInInt;
                if (errorCode != ExceptionErrorCode.UnknownException)
                {
                    webException = new VKeCRM.Framework.Web.Exceptions.BusinessException(errorCode);
                }
                else
                {
                    webException = new UnknownException();
                }
            }
            else
            {
                webException = new UnknownException(faultException);
            }

            return webException;
        }

        protected void CleanUpServiceClient(ICommunicationObject client, IClientChannel channel)
        {
            WcfClientHelper.CleanUpServiceClient(client, channel);
        }
        #endregion

        public string GetVisitorIpAddress()
        {
            string stringIpAddress;
            stringIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (stringIpAddress == null) //may be the HTTP_X_FORWARDED_FOR is null
                stringIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; //we can use REMOTE_ADDR
            else if (stringIpAddress == null)
                stringIpAddress = GetLanIPAddress();

            return stringIpAddress;
        }

        /// <summary>
        /// Get Lan Connect IP address method
        /// </summary>
        /// <returns></returns>
        public string GetLanIPAddress()
        {
            //Get the Host Name
            string stringHostName = Dns.GetHostName();
            //Get The Ip Host Entry
            IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
            //Get The Ip Address From The Ip Host Entry Address List
            System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;
            return arrIpAddress[arrIpAddress.Length - 1].ToString();
        }
    }
}
