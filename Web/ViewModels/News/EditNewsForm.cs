using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels.News;

public class EditNewsForm
{
    public int NewsId { get; set; }
    
    [MinLength(1, ErrorMessage = "Длинна не меньше 1-го символов")]
    [MaxLength(512, ErrorMessage = "Длинна не больше 512-ми символов")]
    public string Title { get; set; }
    
    [MinLength(1, ErrorMessage = "Длинна не меньше 1-го символов")]
    [MaxLength(2048, ErrorMessage = "Длинна не больше 2048-ми символов")]
    public string Description { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Максимум 10 изображений")]
    [MinLength(1, ErrorMessage = "Минимум одно изображение")]
    public List<IFormFile> Images { get; set; }
}