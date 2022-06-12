using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Models;

namespace Web.Services;

public interface IImageService
{
    public Task<Result<byte[]>> GetFirstNewsImage(int newsId);
    Task<string> SaveFile(IFormFile file);
    byte[] GetBytesFrom(IFormFile image);
    bool IsImage(IFormFile image);
}