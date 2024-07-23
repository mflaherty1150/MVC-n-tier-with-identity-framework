using System.ComponentModel.DataAnnotations;

namespace DropDownExample.Models.Movie;

public class MovieCreate
{
    [Required]
    public string Title { get; set;} = string.Empty;
    
    public string Description { get; set;} = string.Empty;
    
    [Required]
    public int GenreId { get; set; } = 0;
}