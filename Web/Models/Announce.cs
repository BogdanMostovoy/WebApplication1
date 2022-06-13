using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class Announce
{
    [Key]
    public int Id { get; set; }

    [MaxLength(512)]
    public string Title { get; set; }

    [MaxLength(4096)]
    public string Description { get; set; }

    public DateTimeOffset DateTimeOfAnnounce { get; set; }
    
    public int AuthorId { get; set; }
    public User Author { get; set; }
    
    /// <summary>
    /// in fact image name
    /// </summary>
    [MaxLength(128)]
    public string ImagePath { get; set; }
}