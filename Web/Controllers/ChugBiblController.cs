using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ChugBiblController : Controller
{
    public int CurrentUserId
    {
        get
        {
            var idClaim = User.Claims.FirstOrDefault(u => u.Type == ClaimsIdentity.DefaultIssuer);
            
            //more info about context to message
            if (idClaim == null)
                throw new Exception("Can't find id claim for user");

            if (!int.TryParse(idClaim.Value, out var id))
                throw new Exception($"Can't parse user id {idClaim.Value}");

            return id;
        }
    }
}