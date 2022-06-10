using System;

namespace WebApplication1.Models;

public class Preview
{
    public int ID { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Date_create { get; set; }

    public string ImagePath { get; set; }
}