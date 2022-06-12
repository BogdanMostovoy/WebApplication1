using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class User
{
    public int Id { get; set; }
    [MaxLength(64)]
    public string Login { get; set; }
    [MaxLength(128)]
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}