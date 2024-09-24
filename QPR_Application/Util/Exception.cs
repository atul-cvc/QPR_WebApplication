using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using IExceptionFilter = Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter;

namespace QPR_Application.Util
{
    public class CustomExceptionFilter : FilterAttribute,IExceptionFilter
    {
      

        public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
        {
            if (context.Exception is NotImplementedException)
            {
            }
            else
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ViewResult()
                {
                    ViewName = "Error"
                };
            }
            //context.HttpContext.Items["Exception"] = context.Exception;

            context.ExceptionHandled = true;
        }
    }
}
