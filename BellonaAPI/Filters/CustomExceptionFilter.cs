using CommonLayer;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BellonaAPI.Filters
{
    public class MyAppException : Exception
    {
        public MyAppException()
        { }

        public MyAppException(string message)
            : base(message)
        { }

        public MyAppException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly ILogger Logger = CommonLayer.Logger.Register(typeof(CustomExceptionFilter));
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty, stackTrace = string.Empty;

            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }

            var exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(MyAppException))
            {
                message = actionExecutedContext.Exception.ToString();
                status = HttpStatusCode.InternalServerError;
            }
            else
            {
                message = actionExecutedContext.Exception.Message;
                status = HttpStatusCode.NotFound;
            }

            var response = new HttpResponseMessage(status)
            {
                Content = new StringContent(message),
                ReasonPhrase = "Error, Please Contact your Administrator."
            };

            Logger.LogError(message + "  (Status Code=" + (int)status + ")" + Environment.NewLine + actionExecutedContext.Exception.StackTrace);
            actionExecutedContext.Response = response;
        }
    }

    

}