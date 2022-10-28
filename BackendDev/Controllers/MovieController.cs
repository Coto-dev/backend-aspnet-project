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
        public MovieController(IMovieService movieservice, IManageFilmsService manageFilmsService)
        {
            _movieService = movieservice;
            _manageFilmService = manageFilmsService;

        }
        [HttpGet]
        [Route("details/{id}")]
        public  ActionResult<MovieDetailsModel> GetMvoieDetails(string Id)
        {
            try
            {
                return Ok(_movieService.GetMovieDetails(Id));
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during adding user");
            }
        }
        [HttpGet]
        [Route("{page}")]
        public ActionResult<MoviesPagedListModel> GetMoviesPage(int page)
        {
            try
            {
                return Ok(_movieService.GetMoviePage(page));
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during adding user");
            }
        }

        [HttpPost]
        [Route("/film/add")]
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
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during adding user");
            }
           
        }

        [HttpPost]
        [Route("/genre/add")]
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
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during adding user");
            }

        }
    }
}
