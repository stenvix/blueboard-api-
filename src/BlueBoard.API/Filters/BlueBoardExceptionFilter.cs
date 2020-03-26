using System.Net;
using BlueBoard.Module.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BlueBoard.API.Filters
{
public class BlueBoardExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<BlueBoardExceptionFilter> logger;

        public BlueBoardExceptionFilter(ILogger<BlueBoardExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context == null) return;
            this.logger.LogError(context.Exception.Message, context.Exception);
            if (context.Exception is BlueBoardValidationException exception)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // this.logger.LogError("Code: {code}, Errors: {@errors}", exception.Code, exception.Errors);
                // var model = new ExceptionModel(exception.Code, exception.Errors);
                // context.Result = new JsonResult(model);
                return;
            }

            HttpStatusCode code;
            bool showTrace = true;
            switch (context.Exception)
            {
                // case NotFoundException e:
                //     {
                //         showTrace = false;
                //         code = HttpStatusCode.NotFound;
                //         break;
                //     }
                // case AuthException e:
                //     {
                //         showTrace = false;
                //         code = HttpStatusCode.Unauthorized;
                //         break;
                //     }
                default:
                    {
                        code = HttpStatusCode.InternalServerError;
                        break;
                    }
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            if (showTrace)
            {
                context.Result = new JsonResult(new
                {
                    error = new[] { context.Exception.Message },
                    stackTrace = context.Exception.StackTrace,
                });
            }
            context.Result = new EmptyResult();
        }
    }
}
