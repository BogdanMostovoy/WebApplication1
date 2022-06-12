using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers;

public class AnnouncesController : Controller
{
    private readonly IAnnouncesService _announcesService;

    public AnnouncesController(IAnnouncesService announcesService)
    {
        _announcesService = announcesService;
    }

    public async Task<IActionResult> AnnounceList() => 
        View(await _announcesService.LightAnnounces());
    
    public IActionResult Create()
    {
        var preview = new Announce();
        return View(preview);
    }

    public IActionResult FullPreview(int id)
    {
        var preview = new Announce();
        return View(preview);
    }


    [HttpPost]
    public IActionResult Create(Announce announce)
    {
        announce.Id = GetHashCode();
        announce.Title = ToString();
        announce.Description = ToString();
        var date = DateTime.Now;
        announce.DateTimeOfAnnounce = date;
        announce.ImagePath = ToString();

        return RedirectToAction(nameof(AnnounceList));
    }
}