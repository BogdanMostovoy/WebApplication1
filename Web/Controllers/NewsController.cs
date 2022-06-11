using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers;

public class NewsController : Controller
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }


    [HttpGet]
    public async Task<IActionResult> NewsList() => View(await _newsService.LightNews());



    public IActionResult DetailedNews(int id)
    {
        var news = new News();
        return View(news);
    }


    [HttpGet]
    public IActionResult CreateNews()
    {
        var news = new News();
        return View(news);
    }
    [HttpPost] 
    public IActionResult CreateNews(News news)
    {
        news.Title = ToString();
        news.Description = ToString();
        var date = DateTime.Now;
        news.DateTimeOfCreate = date;
        news.DateTimeOfUpdate = date;
        news.ImagePath = ToString();

        return RedirectToAction(nameof(NewsList));
    }
}