using DropDownExample.Models.Movie;
using DropDownExample.Data;
using DropDownExample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DropDownExample.Services.Movie;

public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateMovieAsync(MovieCreate model)
    {
        MovieEntity entity = new()
        {
            Title = model.Title,
            Description = model.Description,
            GenreId = model.GenreId,
        };

        _context.Movies.Add(entity);

        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<List<MovieListItem>> GetAllMoviesAsync()
    {
        List<MovieListItem> movies = _context.Movies
            .Include(x => x.Genre)
            .Select(x => new MovieListItem()
            {
                Id = x.Id,
                Title = x.Title,
                GenreName = x.GenreName,
            })
            .ToList();

            return movies;
    }

    public async Task<MovieDetail?> GetMovieDetailAsync(int id)
    {
        MovieEntity? movie = await _context.Movies.FindAsync(id);

        return movie is null ? null : new()
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            GenreId = movie.GenreId,
            GenreName = movie.GenreName,
        };
    }
}