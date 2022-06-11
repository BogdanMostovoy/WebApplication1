using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.News;

namespace Web.Services;

public class NewsService : INewsService
{
    public Task<List<LightNews>> LightNews()
    {
        throw new System.NotImplementedException();
    }

    public Task<Result<DetailedNews>> DetailedNews(int announceId)
    {
        throw new System.NotImplementedException();
    }
}