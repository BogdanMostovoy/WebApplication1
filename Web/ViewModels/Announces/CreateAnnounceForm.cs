using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels.Announces;

public class CreateAnnounceForm
{
    public DateTimeOffset AnnounceDate { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Длинна не меньше 1-го символов")]
    [MaxLength(512, ErrorMessage = "Длинна не больше 512-ми символов")]
    public string Title { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Длинна не меньше 1-го символов")]
    [MaxLength(4096, ErrorMessage = "Длинна не больше 4096-ми символов")]
    public string Description { get; set; }
    
    [Required]
    public IFormFile Image { get; set; }
}