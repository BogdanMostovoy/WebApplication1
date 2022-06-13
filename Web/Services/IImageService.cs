using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Models;

namespace Web.Services;

public interface IImageService
{
    Task<Result<byte[]>> ReadFirstNewsImage(int newsId);
    Task<Result<Dictionary<string, byte[]>>> ReadImages(List<int> imageIds);
    Task<Result<byte[]>> ReadImage(string imageName);
    Task<string> SaveFile(IFormFile file);
    byte[] GetBytesFrom(IFormFile image);
    bool IsImage(IFormFile image);
}