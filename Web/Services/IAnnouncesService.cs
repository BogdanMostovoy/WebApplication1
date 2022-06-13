using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.Announces;

namespace Web.Services;

public interface IAnnouncesService
{
    public Task<List<LightAnnounce>> LightAnnounces();
    public Task<Result<DetailedAnnounce>> DetailedAnnounce(int announceId);
    public Task<Result<int>> Create(int actorId, CreateAnnounceForm form);
    public Task<Result<bool>> Delete(int announceId);
}