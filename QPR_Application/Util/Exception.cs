using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Log the exception (optional)
        // var logger = // inject your logger here
        // logger.LogError(context.Exception, "An unhandled exception occurred.");

        // Set up ViewDataDictionary correctly
        var viewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
        {
            { "ErrorMessage", context.Exception.Message }
        };

        // Set the result to the error view
        context.Result = new ViewResult
        {
            ViewName = "Error", // The name of your error view
            ViewData = viewData
        };

        context.ExceptionHandled = true; // Indicate that the exception is handled
    }
}
