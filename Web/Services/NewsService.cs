using System;
using System.Collections.Generic;
using System.Linq;
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
            var newsImage = await _imageService.ReadFirstNewsImage(item.Id);
            if (newsImage.IsFailure)
                return new(newsImage.ErrorMessage);
            lightNews.Add(new LightNews
            {
                Id = item.Id,
                Title = item.Title,
                MainImage = newsImage.Value,
                PostDate = item.DateTimeOfCreate
            });
        }

        return lightNews;
    }

    public async Task<Result<DetailedNews>> DetailedNews(int newsId)
    {
        var news = await _db.News.AsNoTracking()
            .Include(u => u.Author)
            .Include(u => u.Pictures)
            .FirstOrDefaultAsync(u => u.Id == newsId);
        
        if (news == null)
            return new($"Новость {newsId} не найдена");

        var images = await _imageService.ReadImages(news.Pictures.Select(u => u.Id).ToList());
        if (images.IsFailure)
            return new(images.ErrorMessage);

        return new DetailedNews
        {
            Id = news.Id,
            Title = news.Title,
            Description = news.Description,
            PostDate = news.DateTimeOfCreate,
            Author = news.Author,
            Images = images.Value
        };
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

    public async Task<Result<bool>> Edit(int actorId, EditNewsForm form)
    {
        if (!await _usersService.ExistsBy(actorId))
            return new($"Пользователь {actorId} не найден");
        
        var news = await _db.News.Include(u => u.Pictures)
            .FirstOrDefaultAsync(u => u.Id == form.NewsId);
        if (news == null)
            return new($"Новость {form.NewsId} не найдена");

        news.AuthorId = actorId;
        news.Title = form.Title;
        news.Description = form.Description;
        _db.NewsImages.RemoveRange(news.Pictures);
        if (form.Images.Any())
        {
            var imageNames = new List<string>();
            foreach (var image in form.Images)
                imageNames.Add(await _imageService.SaveFile(image));
        
            _db.NewsImages.AddRange(imageNames.Select(u => new NewsImage
                { Name = u, NewsId = news.Id }));
        }

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<Result<bool>> Delete(int newsId)
    {
        var news = await _db.News.Include(u => u.Pictures)
            .FirstOrDefaultAsync(u => u.Id == newsId);
        if (news == null)
            return new($"Не удалось найти новость {newsId}");

        _db.NewsImages.RemoveRange(news.Pictures);
        _db.News.Remove(news);
        await _db.SaveChangesAsync();

        return true;
    }
}