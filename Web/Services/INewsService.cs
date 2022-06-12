using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.News;

namespace Web.Services;

public interface INewsService
{
    Task<Result<List<LightNews>>> LightNews();
    Task<Result<DetailedNews>> DetailedNews(int newsId);
    Task<Result<int>> Create(int actorId, CreateNewsForm form);
    Task<Result<bool>> Delete(int newsId);
}