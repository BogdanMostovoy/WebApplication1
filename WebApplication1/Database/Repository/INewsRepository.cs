using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Database.Repository
{
    public interface INewsRepository
    {
        ICollection<News> GetNews();
        News GetPost(int NewsId);
    }
}