using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.News;

namespace Web.Services;

public interface INewsService
{
    public Task<List<LightNews>> LightNews();
    public Task<Result<DetailedNews>> DetailedNews(int announceId);
}