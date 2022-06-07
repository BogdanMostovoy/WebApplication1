using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class News
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime Date_create { get; set; }

        public DateTime Date_update { get; set; }
    }
}
