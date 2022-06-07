using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        int Id { get; set; }
        string Name { get; set; }
        private string pass { get; set; }

        bool is_admin { get; set; }
    }
}
