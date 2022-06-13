using System.Linq;
using System.Security.Claims;
using Web.Models;

namespace Web;

public static class IdentityExtensions
{
    public static bool IsAdmin(this ClaimsPrincipal principal)
    {
        var roleClaim = principal.Claims.FirstOrDefault(u => u.Type == ClaimsIdentity.DefaultRoleClaimType);
            
        //more info about context to message
        if (roleClaim == null)
            return false;

        return roleClaim.Value == RoleCodes.Admin;
    }
}