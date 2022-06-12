using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Models;

namespace Web.Services;

public interface IImageService
{
    public Task<Result<byte[]>> ReadFirstNewsImage(int newsId);
    public Task<Result<List<byte[]>>> ReadImages(List<int> imageIds);
    Task<string> SaveFile(IFormFile file);
    byte[] GetBytesFrom(IFormFile image);
    bool IsImage(IFormFile image);
}