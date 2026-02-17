using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using مشروع_قبل_الشغل.Data;

namespace مشروع_قبل_الشغل.Authorizetion
{
    public class PermissionBaseFilters(AppDbContext dbContext) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var Atrributed = (CheckPermissionAtrribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAtrribute);
            if (Atrributed != null)
            {
                var clamisIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (clamisIdentity == null || !clamisIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    var userid = int.Parse(clamisIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var hasPermission = dbContext.Set<UsersPermation>().
                        Any(x => x.userId == userid && x.PermissionId == Atrributed.Permission);
                    if (!hasPermission)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}


