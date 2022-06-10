using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Database.Repository;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class PreviewController : Controller
    {
        private readonly IPreviewServices _previewServices;
        private readonly IPreviewRepository _previewRepository;
        


        public PreviewController(IPreviewRepository previewRepository,IPreviewServices previewServices)
        {
            _previewRepository = previewRepository;
            _previewServices = previewServices;
        }

        public IActionResult previewList()
        {
            var preview = _previewRepository.GetPreviews();
            AllModels models = new AllModels();
            models.previewList = new List<Preview>();
            return View("Index", preview);
        }
        public IActionResult Create()
        {
            var preview = new Preview();
            return View(preview);
        }

        public IActionResult FullPreview(int id)
        {
            var preview = new Preview();
            return View(preview);
        }


        [HttpPost]
        public IActionResult Create(Preview preview)
        {
            preview.ID = GetHashCode();
            preview.Title = ToString();
            preview.Description = ToString();
            var date = DateTime.Now;
            preview.Date_create = date;
            preview.ImagePath = ToString();

            return RedirectToAction(nameof(previewList));
        }
    }
}
