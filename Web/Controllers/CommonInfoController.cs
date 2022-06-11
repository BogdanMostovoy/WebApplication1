using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ALLInfoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}