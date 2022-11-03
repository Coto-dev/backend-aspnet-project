using System.Data;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<MoviesPagedListModel> GetMoviesPage(int? page=1)
        {
            try
            {
                return Ok(_movieService.GetMoviePage((int)page));
            }
            catch (ArgumentException e)
            {
                return NotFound((e.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
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
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        

        [HttpPost]
         [Authorize(Roles = "admin")]
        [Route("film/add")]
        public async Task<IActionResult> AddFilm([FromBody] MovieDetailsModel movieDetailsModelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"];
            var TokenIsValid = await _manageFilmService.CheckToken(token);

            if (!TokenIsValid)
                return BadRequest("невалидный токен");

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
         [Authorize(Roles = "admin")]
        [Route("genre/add")]
        public async Task<IActionResult> AddGenre([FromBody] GenreModel genreModelDTO)
        {
            var token = Request.Headers["Authorization"];
            var TokenIsValid = await _manageFilmService.CheckToken(token);

            if (!TokenIsValid)
                return BadRequest("невалидный токен");

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
       [Authorize(Roles = "admin")]
        [Route("addGenre/{genreId}/toMovie/{movieId}")]
        public async Task<IActionResult> AddGenreToMovie(Guid genreId, Guid movieId)
        {
            var token = Request.Headers["Authorization"];
            var TokenIsValid = await _manageFilmService.CheckToken(token);

            if (!TokenIsValid)
                return BadRequest("невалидный токен");

            await _manageFilmService.AddGenreToFilm(genreId, movieId);
            return Ok();
        }

    }
}
