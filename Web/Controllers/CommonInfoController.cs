using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ALLInfoController : ChugBiblController
{
    public IActionResult Index()
    {
        return View();
    }
}