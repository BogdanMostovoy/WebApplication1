﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class News
{
    [Key]
    public int Id { get; set; }

    [MaxLength(512)]
    public string Title { get; set; }

    [MaxLength(2048)]
    public string Description { get; set; }

    public DateTimeOffset DateTimeOfCreate { get; set; }

    public DateTimeOffset DateTimeOfUpdate { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }

    [MaxLength(128)]
    public string ImagePath { get; set; }
}