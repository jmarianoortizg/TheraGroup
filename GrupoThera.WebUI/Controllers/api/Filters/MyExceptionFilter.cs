using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace GrupoThera.WebUI.Controllers.api.Filters
{
    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(context.Exception.InnerException.InnerException.ToString()),
                ReasonPhrase = "An unhandled exception was thrown"
            };
            context.Response = msg;
        }
    }

}