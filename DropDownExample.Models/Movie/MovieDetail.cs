namespace DropDownExample.Models.Movie;

public class MovieDetail
{
    public int Id { get; set;}

    public string Title { get; set;} = string.Empty;
    public string Description { get; set;} = string.Empty;
    public int GenreId { get; set; } = 0;
    public string GenreName;
}