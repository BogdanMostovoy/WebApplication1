using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels.Auth;

namespace Web.Controllers;

public class AccountController : ChugBiblController
{
    private readonly IAuthService _authService;
    private readonly IUsersService _usersService;

    public AccountController(IAuthService authService, IUsersService usersService)
    {
        _authService = authService;
        _usersService = usersService;
    }
    
    [HttpGet]
    public IActionResult Login() => View();
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Login(LoginModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);

        var verify = await _authService.VerifyPasswordForLogin(model.Login, model.Password);
        if (verify.IsFailure)
        {
            ModelState.AddModelError(string.Empty, verify.ErrorMessage);
            return View(model);
        }

        if (!verify.Value)
        {
            ModelState.AddModelError(string.Empty, "Пароль не верный");
            return View(model);
        }

        var user = await _usersService.ByLogin(model.Login);
        if (user.IsFailure)
        {
            ModelState.AddModelError(string.Empty, user.ErrorMessage);
            return View(model);
        }
            
        
        await Authenticate(user.Value);
        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    private async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimsIdentity.DefaultNameClaimType, user.Login),
            new (ClaimsIdentity.DefaultRoleClaimType, user.Role.Code),
            new (ClaimsIdentity.DefaultIssuer, user.Id.ToString())
        };
        
        var id = new ClaimsIdentity(claims, AuthConstants.CookieName, 
            ClaimsIdentity.DefaultNameClaimType, 
            ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
}