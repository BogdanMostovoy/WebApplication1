using System;

namespace Web.ViewModels.Announces;

public class DetailedAnnounce
{
    public int Id { get; set; }
    public DateTimeOffset AnnounceDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public byte[] Image { get; set; }
}