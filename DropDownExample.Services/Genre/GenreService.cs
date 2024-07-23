using DropDownExample.Models.Genre;
using DropDownExample.Data;
using DropDownExample.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DropDownExample.Services.Genre;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateGenreAsync(GenreCreate model)
    {
        GenreEntity entity = new()
        {
            Name = model.Name,
            Description = model.Description,
        };

        _context.Genres.Add(entity);

        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<List<GenreListItem>> GetAllGenresAsync()
    {
        List<GenreListItem> genres = _context.Genres
            .Select(x => new GenreListItem()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            })
            .ToList();

            return genres;
    }

    public async Task<GenreDetail?> GetGenreDetailAsync(int id)
    {
        GenreEntity? genre = await _context.Genres.FindAsync(id);

        return genre is null ? null : new()
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description,
        };
    }

    public async Task<List<GenreList>> GetGenreSelectListAsync()
    {
        return _context.Genres
            .Select(x => new GenreList()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
    }

}