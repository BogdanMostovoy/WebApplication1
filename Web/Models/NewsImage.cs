using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class NewsImage
{
    public int Id { get; set; }
    [MaxLength(128)]
    public string Name { get; set; }

    public int NewsId { get; set; }
}