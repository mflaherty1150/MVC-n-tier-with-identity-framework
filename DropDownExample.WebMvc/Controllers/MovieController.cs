using Microsoft.AspNetCore.Mvc;
using DropDownExample.Models.Movie;
using DropDownExample.Models.Genre;
using DropDownExample.Services.Movie;
using DropDownExample.Services.Genre;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DropDownExample.WebMvc.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;

    public MovieController(IMovieService movieService, IGenreService genreService)
    {
        _movieService = movieService;
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<MovieListItem> movies = await _movieService.GetAllMoviesAsync();
        return View(movies);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        List<GenreList> genres = await _genreService.GetGenreSelectListAsync();
        var holder = new SelectList(genres, "Id", "Name");
        ViewBag.GenreDropdownList = holder;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(MovieCreate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _movieService.CreateMovieAsync(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        MovieDetail? model = await _movieService.GetMovieDetailAsync(id);

        if (model is null)
            return NotFound();

        return View(model);
    }
}