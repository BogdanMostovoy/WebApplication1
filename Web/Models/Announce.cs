using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class Announce
{
    public int Id { get; set; }

    [MaxLength(512)]
    public string Title { get; set; }

    [MaxLength(4096)]
    public string Description { get; set; }

    public DateTimeOffset DateTimeOfCreate { get; set; }
    
    
    [MaxLength(128)]
    public string ImagePath { get; set; }
}