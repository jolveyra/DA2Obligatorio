using LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManagementApi.Filters
{
    public class AuthenticationFilter : Attribute, IActionFilter
    {
        public List<string> Roles { get; set; }

        public AuthenticationFilter(string[] roles)
        {
            Roles = new List<string>(roles);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string header = context.HttpContext.Request.Headers["Authorization"];
            Guid token = Guid.Parse(header);
            if (header is null)
            {
                context.Result = new ObjectResult(new { ErrorMessage = "Authorization header is missing" }) { StatusCode = 401 };
            }
            else
            {
                if (Roles.Count > 0)
                {
                    var authenticationService = (IAuthorizationLogic)context.HttpContext.RequestServices.GetService(typeof(IAuthorizationLogic));
                    string userRole = authenticationService.GetUserRoleByToken(token);

                    if (!Roles.Contains(userRole))
                    {
                        context.Result = new ObjectResult(new { ErrorMessage = "Unauthorized access" }) { StatusCode = 403 };
                        return;
                    }
                }
                else
                {
                    context.Result = new ObjectResult(new { ErrorMessage = "Unauthorized access" }) { StatusCode = 403 };
                }
            }

        }
    }
}
