using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Models;
using Web.ViewModels.News;

namespace Web.Services;

public class NewsService : INewsService
{
    private readonly ApplicationContext _db;
    private readonly IImageService _imageService;
    private readonly IUsersService _usersService;

    public NewsService(ApplicationContext db, 
        IImageService imageService,
        IUsersService usersService)
    {
        _db = db;
        _imageService = imageService;
        _usersService = usersService;
    }

    public async Task<Result<List<LightNews>>> LightNews()
    {
        var news = await _db.News.ToListAsync();
        var lightNews = new List<LightNews>();
        foreach (var item in news)
        {
            var newsImage = await _imageService.GetFirstNewsImage(item.Id);
            if (newsImage.IsFailure)
                return new(newsImage.ErrorMessage);
            lightNews.Add(new LightNews
            {
                Title = item.Title,
                MainImage = newsImage.Value,
                PostDate = item.DateTimeOfCreate
            });
        }

        return lightNews;
    }

    public Task<Result<DetailedNews>> DetailedNews(int announceId)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Result<int>> Create(int actorId, CreateNewsForm form)
    {
        var actorExists = await _usersService.ExistsBy(actorId);
        if (!actorExists)
            return new("Пользователь для создания новости не найден");

        var currentDateTime = DateTimeOffset.Now;
        var news = new News
        {
            Title = form.Title,
            Description = form.Description,
            DateTimeOfCreate = currentDateTime,
            DateTimeOfUpdate = currentDateTime,
            AuthorId = actorId,
            Pictures = new List<NewsImage>()
        };

        _db.News.Add(news);
        await _db.SaveChangesAsync();

        foreach (var image in form.Images)
            if (!_imageService.IsImage(image))
                return new($"Файл {image.Name} не являеться поддерживаемым изображением");

        var images = new List<NewsImage>();
        foreach (var image in form.Images)
            images.Add(new NewsImage
            {
                Name = await _imageService.SaveFile(image),
                NewsId = news.Id
            });

        _db.NewsImages.AddRange(images);
        await _db.SaveChangesAsync();

        return news.Id;
    }
}