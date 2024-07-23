using DropDownExample.Models.Movie;

namespace DropDownExample.Services.Movie;

public interface IMovieService
{
    Task<bool> CreateMovieAsync(MovieCreate model);
    Task<List<MovieListItem>> GetAllMoviesAsync();
    Task<MovieDetail?> GetMovieDetailAsync(int id);
}