using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Helpers
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new Response();
            //if (context.Exception is HttpResponseException exception)
            //{
            //    res.SetError(exception.StatusCode, exception.Value);
            //}
            //else
            //{
            //    res.SetError(StatusCodes.Status400BadRequest, context.Exception.Message);
            //}
            response.SetError("Lỗi không xác định");
            ActionResult<Response> result = response ;
            context.Result = result.Result;
        }
    }
}
