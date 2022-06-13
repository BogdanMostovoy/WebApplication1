using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Models;
using Web.ViewModels.Announces;

namespace Web.Services;

public class AnnouncesService : IAnnouncesService
{
    private readonly ApplicationContext _db;
    private readonly IUsersService _usersService;
    private readonly IImageService _imageService;

    public AnnouncesService(ApplicationContext db,
        IUsersService usersService,
        IImageService imageService)
    {
        _db = db;
        _usersService = usersService;
        _imageService = imageService;
    }
    
    public async Task<List<LightAnnounce>> LightAnnounces()
    {
        var announces = await _db.Announces.ToListAsync();

        var lightAnnounces = new List<LightAnnounce>();
        foreach (var announce in announces)
            lightAnnounces.Add(new LightAnnounce
            {
                Id = announce.Id,
                AnnounceDate = announce.DateTimeOfAnnounce,
                Title = announce.Title,
                Image = (await _imageService.ReadImage(announce.ImagePath)).Value
            });

        return lightAnnounces;
    }

    public async Task<Result<DetailedAnnounce>> DetailedAnnounce(int announceId)
    {
        var announce = await _db.Announces.FirstOrDefaultAsync(u => u.Id == announceId);
        if (announce == null)
            return new($"Аннонс {announceId} не найден");

        return new DetailedAnnounce
        {
            Id = announce.Id,
            AnnounceDate = announce.DateTimeOfAnnounce,
            Title = announce.Title,
            Image = (await _imageService.ReadImage(announce.ImagePath)).Value,
            Description = announce.Description
        };
    }

    public async Task<Result<int>> Create(int actorId, CreateAnnounceForm form)
    {
        if (!await _usersService.ExistsBy(actorId))
            return new($"Пользователь {actorId} не найден");
        
        var imageName = await _imageService.SaveFile(form.Image);
        var announce = new Announce
        {
            Title = form.Title,
            Description = form.Description,
            DateTimeOfAnnounce = form.AnnounceDate,
            AuthorId = actorId,
            ImagePath =  imageName
        };

        _db.Announces.Add(announce);
        await _db.SaveChangesAsync();
        return announce.Id;
    }

    public async Task<Result<bool>> Delete(int announceId)
    {
        var announce = await _db.Announces.FirstOrDefaultAsync(u => u.Id == announceId);
        if (announce == null)
            return new($"Аннонс {announceId} не найден");

        _db.Announces.Remove(announce);
        return true;
    }
}