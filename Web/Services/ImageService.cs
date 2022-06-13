using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Models;

namespace Web.Services;

public class ImageService : IImageService
{
    private readonly ApplicationContext _db;
    private readonly IWebHostEnvironment _environment;
    private readonly string _pathToImages;

    private static readonly List<List<string>> PictureExtensions = new ()
    {
        "89 50 4E 47".Split().ToList(),
        "FF D8 FF DB".Split().ToList(),
        "FF D8 FF E0".Split().ToList()
    };

    public ImageService(ApplicationContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
        _pathToImages = Path.Combine(_environment.WebRootPath, "Uploads");
    }

    public async Task<Result<byte[]>> ReadFirstNewsImage(int newsId)
    {
        var firstImageName = await _db.NewsImages
            .Where(u => u.NewsId == newsId)
            .OrderBy(u => u.Id)
            .Select(u => u.Name)
            .FirstOrDefaultAsync();
        if (firstImageName == null)
            return new($"Фото для новости не найдено {newsId}");

        byte[] image;
        var path = Path.Combine(_pathToImages, firstImageName);
        try
        {
            image = await File.ReadAllBytesAsync(path);
        }
        catch (Exception)
        {
            return new($"Нельзя прочитать файл {firstImageName}");
        }

        return image;
    }

    public async Task<Result<Dictionary<string, byte[]>>> ReadImages(List<int> imageIds)
    {
        var imageNames = await _db.NewsImages
            .Where(u => imageIds.Contains(u.Id))
            .OrderBy(u => u.Id)
            .Select(u => u.Name)
            .ToListAsync();
        
        var images = new Dictionary<string, byte[]>();
        
        foreach (var name in imageNames)
        {
            var path = Path.Combine(_pathToImages, name);
            try
            {
                images.Add(name, await File.ReadAllBytesAsync(path));
            }
            catch (Exception)
            {
                return new($"Нельзя прочитать файл {name}");
            }
            
        }

        if (images.Count != imageIds.Count)
            return new($"Не удалось найти все фото({string.Join(",", imageIds.Select(u => u.ToString()))})");
        
        return images;
    }

    public async Task<Result<byte[]>> ReadImage(string imageName)
    {
        var path = Path.Combine(_pathToImages, imageName);
        byte[] image;
        try
        {
            image = await File.ReadAllBytesAsync(path);
        }
        catch (Exception)
        {
            return new($"Нельзя прочитать файл {imageName}");
        }

        return image;
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        if (!Directory.Exists(_pathToImages))
            Directory.CreateDirectory(_pathToImages);
        
        
        var fileName = Guid.NewGuid() + ".png";
        await using var stream = new FileStream(Path.Combine(_pathToImages, fileName), FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    } 

    public byte[] GetBytesFrom(IFormFile image)
    {
        byte[] imageData;
        if (image != null)
            using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                imageData = binaryReader.ReadBytes((int)image.Length);
        else
            throw new NullReferenceException("Input object is null");
            
        return imageData;
    }

    public bool IsImage(IFormFile image)
    {
        var file = GetBytesFrom(image);
        var fileHead = new List<string>();
        for (var i = 0; i < 4; i++)
            fileHead.Add(file[i].ToString("X2"));
        
        foreach (var extensionHead in PictureExtensions)
            if (!extensionHead.Except(fileHead).Any())
                return true;

        return false;
    }
}