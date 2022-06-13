using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.ViewModels.Announces;

namespace Web.Controllers;

public class AnnouncesController : ChugBiblController
{
    private readonly IAnnouncesService _announcesService;

    public AnnouncesController(IAnnouncesService announcesService)
    {
        _announcesService = announcesService;
    }

    public async Task<IActionResult> List() => 
        View(await _announcesService.LightAnnounces());
    


    [HttpGet]
    public async Task<IActionResult> Detailed(int announceId)
    {
        var announce = await _announcesService.DetailedAnnounce(announceId);
        return View(announce.Value);
    }

    [HttpGet]
    public IActionResult Create() => View(new CreateAnnounceForm());
    
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateAnnounceForm form)
    {
        if (!ModelState.IsValid)
            return View(form);
        
        var announceId = await _announcesService.Create(CurrentUserId.Value, form);
        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int announceId)
    {
        var delete = await _announcesService.Delete(announceId);
        return RedirectToAction("List");
    }
}