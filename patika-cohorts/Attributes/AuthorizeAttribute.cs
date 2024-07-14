using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using patika_cohorts.Services;

namespace patika_cohorts.Attributes;

public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userService = context.HttpContext.RequestServices.GetService<IUserService>();
        var username = context.HttpContext.Request.Headers["Username"].ToString();
        var password = context.HttpContext.Request.Headers["Password"].ToString();

        if (!userService.ValidateUser(username, password))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}