using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ChugBiblController : Controller
{
    public int? CurrentUserId
    {
        get
        {
            var idClaim = User.Claims.FirstOrDefault(u => u.Type == ClaimsIdentity.DefaultIssuer);
            
            //more info about context to message
            if (idClaim == null)
                return null;

            if (!int.TryParse(idClaim.Value, out var id))
                return null;

            return id;
        }
    }
    
    public string CurrentUserRoleCode
    {
        get
        {
            var roleClaim = User.Claims.FirstOrDefault(u => u.Type == ClaimsIdentity.DefaultRoleClaimType);
            
            //more info about context to message
            if (roleClaim == null)
                return null;

            return roleClaim.Value;
        }
    }
}