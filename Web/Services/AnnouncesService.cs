using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.Announces;

namespace Web.Services;

public class AnnouncesService : IAnnouncesService
{
    public Task<List<LightAnnounce>> LightAnnounces()
    {
        throw new System.NotImplementedException();
    }

    public Task<Result<DetailedAnnounce>> DetailedAnnounce(int announceId)
    {
        throw new System.NotImplementedException();
    }
}