using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Database.Repository
{
    public interface IPreviewRepository
    {
        ICollection<Preview> GetPreviews();
        Preview GetPreview(int id);
    }
}
