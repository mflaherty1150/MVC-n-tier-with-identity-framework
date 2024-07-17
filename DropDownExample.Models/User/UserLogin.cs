using System.ComponentModel.DataAnnotations;

namespace DropDownExample.Models.User;

public class UserLogin
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}