using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DropDownExample.Data.Entities;

public class MovieEntity
{
    [Key]
    public int Id { get; set;}

    [Required]
    public string Title { get; set;} = string.Empty;
    
    public string Description { get; set;} = string.Empty;
    
    [ForeignKey(nameof(Genre))]
    public int GenreId { get; set; } = 1;
    public GenreEntity Genre { get; set; }

    public string GenreName
    {
        get
        {
            return Genre.Name;
        }
    }
}