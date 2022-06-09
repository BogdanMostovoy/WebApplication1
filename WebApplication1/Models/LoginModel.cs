using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
   
        public class LoginModel
        {
            [Required(ErrorMessage = "Не указан Логин")]
            public string login { get; set; }

            [Required(ErrorMessage = "Не указан пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    
}
