using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels.News;

namespace Web.Controllers;

public class NewsController : ChugBiblController
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var lightNews = await _newsService.LightNews();
        if (lightNews.IsFailure)
        {
            ModelState.AddModelError(string.Empty, lightNews.ErrorMessage);
            return View(new List<LightNews>());
        }

        return View(lightNews.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Detailed(int newsId)
    {
        var news = await _newsService.DetailedNews(newsId);
        if (news.IsFailure)
            ViewBag.ErrorMessage = news.ErrorMessage;
        
        return View(news.Value);
    }

    [HttpGet]
    [Authorize(Roles = RoleCodes.Admin)]
    public IActionResult Create()
    {
        return View(new CreateNewsForm());
    }


    [HttpPost] 
    [Authorize(Roles = RoleCodes.Admin)]
    public async Task<IActionResult> Create([FromForm]CreateNewsForm createForm)
    {
        if (!ModelState.IsValid)
            return View(createForm);
        
        var creation = await _newsService.Create(CurrentUserId.Value, createForm);
        if (creation.IsFailure)
        {
            ModelState.AddModelError(string.Empty, creation.ErrorMessage);
            return View(createForm);
        }

        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    [Authorize(Roles = RoleCodes.Admin)]
    public async Task<IActionResult> Delete(int newsId)
    {
        //TODO: should be log if error
        var delete = await _newsService.Delete(newsId);

        return RedirectToAction("List", "News");
    }
}