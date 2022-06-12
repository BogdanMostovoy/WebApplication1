using System;
using System.Collections.Generic;

namespace Web.ViewModels.News;

public class DetailedNews
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset PostDate { get; set; }
    public List<byte[]> Images { get; set; }
}