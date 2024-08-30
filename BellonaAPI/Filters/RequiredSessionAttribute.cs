using CommonLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BellonaAPI.Filters
{
    /// <summary>
    /// Filter that gets session context from request if HttpContext.Current is null.
    /// </summary>
    public class RequireSessionAttribute : ActionFilterAttribute
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(RequireSessionAttribute));

        /// <summary>
        /// Runs before action
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (HttpContext.Current == null)
            {
                if (actionContext.Request.Properties.ContainsKey("MS_HttpContext"))
                {
                    HttpContext.Current = ((HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).ApplicationInstance.Context;
                }
            }
        }
    }

    public class ValidationActionFilter : ActionFilterAttribute
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(ValidationActionFilter));
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
                Logger.LogError("One or more entered values are invalid, please check" + Environment.NewLine + actionContext.Response.RequestMessage);
                throw new MyAppException("One or more entered values are invalid, please check" + Environment.NewLine + actionContext.Response.RequestMessage);
            }
        }
    }

}