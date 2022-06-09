using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Логин!")]
        public string login { get; set; }
        [Required(ErrorMessage = "Не указан пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введён неверно!")]
        public string ConfirmPassword { get; set; }
    }
}