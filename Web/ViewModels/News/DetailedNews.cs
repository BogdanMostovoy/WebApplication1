using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels.News;

public class DetailedNews
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset PostDate { get; set; }
    public User Author { get; set; }
    public Dictionary<string, byte[]> Images { get; set; }
}