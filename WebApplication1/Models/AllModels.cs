﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AllModels
    {
        public List<News> newsList { get; set; }

        public List<Preview> previewList { get; set; }



    }
}
