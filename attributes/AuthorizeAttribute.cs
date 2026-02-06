using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var teacherId = context.HttpContext.Items["TeacherId"];
        if (teacherId == null)
        {
            // Not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) 
            { 
                StatusCode = StatusCodes.Status401Unauthorized 
            };
        }
    }
}
