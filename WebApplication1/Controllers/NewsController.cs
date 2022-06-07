using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }


        [HttpGet]
        public IActionResult newsList()
        {
           AllModels model = new AllModels();
            

            model.newsList = _newsService.GetNewsRecord().ToList();
            return View("NewsList" ,model);
        }


        public IActionResult Create()
        {
            var news = new News();
            return View(news);
        }

        public IActionResult FullNews(int id)
        {
            var news = new News();
            return View(news);
        }


       [HttpPost] 
       public IActionResult Create(News news)
        {
            var date = DateTime.Now;
            news.Date_create = date;
            news.Date_update = date;
            return RedirectToAction(nameof(newsList));
        }
    }
}
