using System;

namespace Web.ViewModels.Announces;

public class LightAnnounce
{
    public int Id { get; set; }
    public DateTimeOffset AnnounceDate { get; set; }
    public string Title { get; set; }
    public byte[] Image { get; set; }
}