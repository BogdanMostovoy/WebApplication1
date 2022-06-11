using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(512)]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        public DateTime DateOfCreate { get; set; }

        public DateTime DateOfUpdate { get; set; }

        [MaxLength(128)]
        public string ImagePath { get; set; }
    }
}
