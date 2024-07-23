using DropDownExample.Models.Genre;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DropDownExample.Services.Genre;

public interface IGenreService
{
    Task<bool> CreateGenreAsync(GenreCreate model);
    Task<List<GenreListItem>> GetAllGenresAsync();
    Task<GenreDetail?> GetGenreDetailAsync(int id);
    Task<List<GenreList>> GetGenreSelectListAsync();
}