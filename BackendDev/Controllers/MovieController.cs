using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendDev.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : Controller
    {
        private IMovieService _movieService;
        private IManageFilmsService _manageFilmService;
        private ILogger<MovieController> _logger;
        public MovieController(IMovieService movieservice, IManageFilmsService manageFilmsService, ILogger<MovieController> logger)
        {
            _movieService = movieservice;
            _manageFilmService = manageFilmsService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{page}")]
        public ActionResult<MoviesPagedListModel> GetMoviesPage(int page)
        {
            try
            {
                return Ok((_movieService.GetMoviePage(page)));
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during getting MoviesPagedListModel");
            }
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<ActionResult<MovieDetailsModel>> GetMvoieDetails(Guid id)
        {
            try
            {
                return Ok((await _movieService.GetMovieDetails(id)));
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during getting MovieDetailsModel");
            }
        }
        

        [HttpPost]
        [Route("film/add")]
        public async Task<IActionResult> AddFilm([FromBody] MovieDetailsModel movieDetailsModelDTO)
        {
            try
            {
             await _manageFilmService.AddFilm(movieDetailsModelDTO);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during adding film");
            }
           
        }

        [HttpPost]
        [Route("genre/add")]
        public async Task<IActionResult> AddGenre([FromBody] GenreModel genreModelDTO)
        {
            try
            {
                await _manageFilmService.AddGenre(genreModelDTO);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during adding genre");
            }

        }
        [HttpPost]
        [Route("addGenre/{genreId}/toMovie/{movieId}")]
        public async Task<IActionResult> AddGenreToMovie(Guid genreId, Guid movieId)
        {
            await _manageFilmService.AddGenreToFilm(genreId, movieId);
            return Ok();
        }

    }
}
