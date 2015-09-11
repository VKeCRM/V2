using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Reflection;
using System.Linq;
using System.Web.Routing;
using System.Web;
using VKeCRM.Framework.Mvc.Exceptions;
using WebHttpContext = System.Web.HttpContext;
using VKeCRM.Framework.Mvc.Security;

namespace VKeCRM.Framework.Mvc
{
    [ActionFlow(Order = 99)]
    [Authorize(Order = 2)]
    [ErrorHandler(Order = 1)]
    public abstract class ControllerBase : System.Web.Mvc.AsyncController, IAuthorization, IErrorHanlder, IFlowHandler
    {
        private const char SplitChar = '$';
        private const string UnHandledErrorMessage = "We are sorry, there is an unknown error occurred. Please wait a few minutes and try again later.";

        private static Dictionary<string, ParameterInfo[]> _parameterCacher = null;

        static ControllerBase()
        {
            _parameterCacher = new Dictionary<string, ParameterInfo[]>();
        }

        private ParameterInfo[] GetParameterStruct(Type controllerType, string methodName)
        {
            string key = string.Format("{0}/{1}", controllerType.FullName, methodName);

            lock (_parameterCacher)
            {
                if (!_parameterCacher.ContainsKey(key))
                {
                    MethodInfo methodInfo = controllerType.GetMethod(methodName);
                    ParameterInfo[] methodParamterInfos = methodInfo.GetParameters();

                    _parameterCacher.Add(key, methodParamterInfos);
                }

                return _parameterCacher[key];
            }
        }

        #region Authentiaction
        public virtual System.Web.SessionState.HttpSessionState MySession
        {
            get
            {
                return System.Web.HttpContext.Current.Session;
            }
        }

        /// <summary>
        /// Implement IVKeCRMAuthorization Interfacr
        /// Default RequireAuthenticationLevel
        /// If you want to changed the authentication level, you must override it.
        /// </summary>
        public virtual AuthenticationLevel RequireAuthenticationLevel
        {
            get
            {
                return AuthenticationLevel.CookieAuthenticated;
            }
        }

        protected virtual void OnLogError(Exception ex)
        {

        }

        private bool _isAlreadyAuthorized = false;

        /// <summary>
        /// We use this flag to make sure that the method authorized will replace the controller authroize.
        /// And the authorize will execute only once.
        /// </summary>
        public bool IsAlreadyAuthorized
        {
            get
            {
                return _isAlreadyAuthorized;
            }
            set
            {
                _isAlreadyAuthorized = value;
            }
        }

        public abstract bool IsCookieAuthentication();

        public abstract bool IsFullAuthentication();

        public abstract bool IsIPAuthentication();

        public abstract string GetKickOutInfo();

        public abstract string GetFullLoginUrl();

        #endregion

        #region Dynamic execute methods in controller
        public System.Web.Mvc.ActionResult DynamicExecute()
        {
            Dictionary<string, ActionResult> result = new Dictionary<string, ActionResult>();

            for (int i = 0; i < Request.Form.Keys.Count; i++)
            {
                try
                {
                    string formKey = Request.Form.Keys[i];
                    string formValue = Request.Form[formKey];

                    string[] keys = formKey.Split(SplitChar);
                    //If this is not method entry, ignore it.
                    if (keys.Length != 3)
                        continue;

                    // What are keys?
                    // Keys format: [Control ID (at page)]$[controller name]$[method name]
                    string controllerName = keys[1];
                    string methodName = keys[2];

                    // What are values?
                    // Values format: [controller full type name]$[Json StoreID]
                    string[] values = formValue.Split(SplitChar);

                    string controllerFullTypeName = values[0];
                    string dataStoreID = values[1];

                    Type controllerType = Type.GetType(controllerFullTypeName);

                    //Get specific method paramters from request
                    Dictionary<string, object> sourceParamters = new Dictionary<string, object>();
                    for (int j = i + 1; j < Request.Form.Keys.Count; j++)
                    {
                        string[] nextKeys = Request.Form.Keys[j].Split(SplitChar);
                        if (nextKeys.Length == 4)
                        {
                            sourceParamters.Add(nextKeys[3], Request.Form[j]);
                            i = j; // need to jump Paramter keys
                        }
                        else
                            break;
                    }

                    Dictionary<string, object> methodParamters = new Dictionary<string, object>();

                    // read ParamterInfos from cache
                    ParameterInfo[] methodParamterInfos = GetParameterStruct(controllerType, methodName);

                    //Maaping the reqeust paramter to method paramter.
                    foreach (ParameterInfo info in methodParamterInfos)
                    {
                        if (sourceParamters.ContainsKey(info.Name))
                        {
                            string paramterValue = (string)sourceParamters[info.Name];
                            if (TypeDescriptor.GetConverter(info.ParameterType).CanConvertFrom(typeof(string)))
                            {
                                methodParamters.Add(info.Name, TypeDescriptor.GetConverter(info.ParameterType).ConvertFromString(paramterValue));
                            }
                        }
                        else
                        {
                            throw new ArgumentException(string.Format("Paramter:{0} not found in reqeust", info.Name));
                        }
                    }

                    ActionResult actionResult = new EmptyResult();
                    try
                    {
                        actionResult = InvokeAction(controllerType, methodName, methodParamters);
                    }
                    catch { }

                    //Go
                    result.Add(dataStoreID, actionResult);
                }
                catch (Exception ex)
                {
                    //ignore exception, because this is a action chain.
                    if (!result.ContainsKey("undefine"))
                    {
                        result.Add("undefine", new JsonResult(MvcErrorType.Normal, ex.Message));
                    }
                }
            }

            System.Web.Mvc.JsonResult jsonResult = new System.Web.Mvc.JsonResult();
            jsonResult.Data = result.ToArray();

            return jsonResult;
        }

        /// <summary>
        /// Invoke spedifci action
        /// </summary>
        /// <param name="controllerType"></param>
        /// <param name="actionName"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        private ActionResult InvokeAction(Type controllerType, string actionName, IDictionary<string, object> paramters)
        {
            //Retrieve useful paramter
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
            HttpContextBase httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
            RouteData routeData = new RouteData();
            RequestContext requestContext = new RequestContext(httpContext, routeData);
            ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)Activator.CreateInstance(controllerType));
            //If we don't do this, the controller method will lost httpcontext, server and so on.
            controllerContext.Controller.ControllerContext = controllerContext;

            //Get the action
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);
            object result = null;
            if (actionDescriptor != null)
            {
                FilterInfo filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));

                // if the current controller implements one of the filter interfaces, it should be added to the list at position 0
                System.Web.Mvc.ControllerBase controller = controllerContext.Controller;
                AddControllerToFilterList(controller, filters.ActionFilters);
                AddControllerToFilterList(controller, filters.ResultFilters);
                AddControllerToFilterList(controller, filters.AuthorizationFilters);
                AddControllerToFilterList(controller, filters.ExceptionFilters);

                try
                {
                    //Invoke the authorize filters.
                    AuthorizationContext authContext = InvokeAuthorizationFilters(controllerContext, filters.AuthorizationFilters, actionDescriptor);

                    if (authContext.Result == null)
                    {
                        result = actionDescriptor.Execute(controllerContext, paramters);
                    }
                    else
                    {
                        result = authContext.Result;
                    }
                }
                catch (Exception ex)
                {
                    // something blew up, so execute the exception filters
                    ExceptionContext exceptionContext = InvokeExceptionFilters(controllerContext, filters.ExceptionFilters, ex);
                    if (!exceptionContext.ExceptionHandled)
                    {
                        throw;
                    }
                    else
                    {
                        result = exceptionContext.Result;
                    }
                }

            }

            ActionResult actionResult;
            //Validation result.
            if (result == null || !(result is ActionResult))
            {
                actionResult = new EmptyResult();
            }
            else
            {
                actionResult = (ActionResult)result;
            }

            return actionResult;
        }

        private void AddControllerToFilterList<TFilter>(System.Web.Mvc.ControllerBase controller, IList<TFilter> filterList) where TFilter : class
        {
            TFilter controllerAsFilter = controller as TFilter;
            if (controllerAsFilter != null)
            {
                filterList.Insert(0, controllerAsFilter);
            }
        }

        private ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext, IList<IExceptionFilter> filters, Exception exception)
        {
            ExceptionContext context = new ExceptionContext(controllerContext, exception);
            foreach (IExceptionFilter filter in filters)
            {
                filter.OnException(context);
            }

            return context;
        }

        private AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
        {
            AuthorizationContext context = new AuthorizationContext(controllerContext, actionDescriptor);
            foreach (IAuthorizationFilter filter in filters)
            {
                filter.OnAuthorization(context);
                // short-circuit evaluation
                if (context.Result != null)
                {
                    break;
                }
            }

            return context;
        }
        #endregion

        #region IAuthorization Members

        public virtual void VerifyToken(AuthorizationContext context)
        {
            var request = WebHttpContext.Current.Request;

            if (request.ServerVariables["Request_Method"].Equals("POST"))
            {
                var tokenFromParamter = request.Form[CsrfToken.TokenName];

                if (!(request.Cookies[CsrfToken.TokenName] != null &&
                    tokenFromParamter != null &&
                    request.Cookies[CsrfToken.TokenName].Value == tokenFromParamter))
                {
                    context.Result = new JsonResult(MvcErrorType.TokenValidationgFailed, "Token validation failed. Please wait a few minutes and try again later.");

                    VKeCRM.Common.Logging.LoggerManager.VKeCRMCommonLogger.Error
                        (string.Format("CSRF token validation failed. Cookie: {0} Form: {1}", request.Cookies[CsrfToken.TokenName] != null ? request.Cookies[CsrfToken.TokenName].Value : "NULL", tokenFromParamter));
                }

                if (string.IsNullOrEmpty(tokenFromParamter))
                {
                    VKeCRM.Common.Logging.LoggerManager.VKeCRMCommonLogger.ErrorFormat("To avoid token missing in normal case, we reset CSRF token to cookie again.");
                    CsrfToken.SetNewCsrfCookie(System.Web.HttpContext.Current.Response.Cookies, (string)System.Web.HttpContext.Current.Session[CsrfToken.TokenName]);
                }
            }
        }

        public virtual void DoAuthentication(AuthenticationLevel targetAuthenticationLevel)
        {
            //This logic to ensure we only authorize one time for each action.
            if (this.IsAlreadyAuthorized)
                return;
            else
                this.IsAlreadyAuthorized = true;

            //Set the intialize value to "true", which can cover the "None" AuthentactinoType
            bool isAuthenticationSuccessful = true;
            switch (targetAuthenticationLevel)
            {
                case AuthenticationLevel.FullyAuthenticated:
                    {
                        isAuthenticationSuccessful = this.IsFullAuthentication();
                        break;
                    }
                case AuthenticationLevel.CookieAuthenticated:
                    {
                        isAuthenticationSuccessful = this.IsCookieAuthentication();
                        break;
                    }
                case AuthenticationLevel.IPAuthenticated:
                    {
                        isAuthenticationSuccessful = this.IsIPAuthentication();
                        break;
                    }
            }

            //If authentication faled, redirect to login page.
            if (!isAuthenticationSuccessful)
            {
                throw new MvcAuthorizeException("User is not authorized!");
            }

            if (targetAuthenticationLevel == AuthenticationLevel.CookieAuthenticated ||
                targetAuthenticationLevel == AuthenticationLevel.FullyAuthenticated)
            {
                string reasonCode = this.GetKickOutInfo();

                if (!string.IsNullOrEmpty(reasonCode))
                    throw new MvcKickOutException("Kick Out member", reasonCode);
            }
        }

        public virtual void VerifyPermission(AuthorizationContext context)
        {
            if (PermessionManager.Instance.Enabled)
            {
                string action = string.Format("/{0}/{1}/VKeCRM.api", context.RouteData.GetRequiredString("Controller"), context.RouteData.GetRequiredString("Action"));

                if (!PermessionManager.Instance.HasPermission(action, true))
                    throw new PermissionDeniedException(string.Empty);
            }
        }

        #endregion

        #region IErrorHanlder Members
        /// <summary>
        /// Implement IVKeCRMErrorHanlder interface.
        /// Default strategy for error handling.
        /// Set the error flag and attach error message
        /// If you want to change the error handling strategy, you must override it.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public virtual ActionResult HandleError(Exception ex)
        {
            //VKeCRM.Framework.Web.Exceptions.BusinessException
            if (ex is MvcExceptionBase)
            {
                return (ex as MvcExceptionBase).ToJsonResult();
            }
            else
            {
                OnLogError(ex);

                var unHandledError = HttpContext.GetGlobalResourceObject("Messages", "MvcUnHandledError") as string;
                unHandledError = string.IsNullOrEmpty(unHandledError) ? UnHandledErrorMessage : unHandledError;

                return new JsonResult
                {
                    Error = (int)MvcErrorType.Normal,
                    ErrorMessage = unHandledError
                };
            }
        }

        #endregion

        #region IFlowHandler Members

        public virtual void BeforeActionExecuting(ControllerContext context)
        {

            //if(MySession["MySession"] as 

        }

        public virtual void AfterActionExecuted(ActionExecutedContext context)
        {
        }

        #endregion



        //public virtual JsonResult GetEmptyResult()
        //{
        //    return new JsonResult(null);
        //}
    }
}
