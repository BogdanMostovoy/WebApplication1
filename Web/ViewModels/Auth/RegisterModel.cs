using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Auth;

public class RegisterModel
{
    [Required(ErrorMessage = "Не указан Логин!")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Не указан пароль!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароль введён неверно!")]
    public string ConfirmPassword { get; set; }
}