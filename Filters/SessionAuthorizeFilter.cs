using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Login.Filters
{
    public class SessionAuthorizeFilter : IAuthorizationFilter
    {
         public void OnAuthorization(AuthorizationFilterContext context)
    {
        var session = context.HttpContext.Session;
        var isAuthorized = session.TryGetValue("IsLoggedIn", out byte[] isLoggedIn) &&
                           isLoggedIn[0] == 1; // Assuming a boolean value stored in session

        if (!isAuthorized)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
        }
    }
    }
}