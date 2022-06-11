using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels;

public class AllModels
{
    public List<Models.News> NewsList { get; set; }

    public List<Announce> PreviewList { get; set; }
    
}