using System;

namespace Web.ViewModels.News;

public class LightNews
{
    public int Id { get; set; }
    public string Title { get; set; }
    public byte[] MainImage { get; set; }
    public DateTimeOffset PostDate { get; set; }
}