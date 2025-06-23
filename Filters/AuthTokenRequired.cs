using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthTokenRequiredAttribute : Attribute, IPageFilter
{
    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var token = context.HttpContext.Session.GetString("AuthToken");
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToPageResult("/Login");
        }
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
}