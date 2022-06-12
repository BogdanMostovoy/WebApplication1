using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class Role
{
    public int Id { get; set; }
    [MaxLength(64)]
    public string Code { get; set; }
}