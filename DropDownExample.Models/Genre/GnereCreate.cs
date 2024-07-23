using System.ComponentModel.DataAnnotations;

namespace DropDownExample.Models.Genre;

public class GenreCreate
{
    [Required]
    public string Name { get; set;} = string.Empty;

    public string Description { get; set;} = string.Empty;
}