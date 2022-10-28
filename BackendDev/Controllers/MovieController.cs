using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : Controller
    {
        private IMovieService _movieService;

        public MovieController(IMovieService movieservice)
        {
            _movieService = movieservice;

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
        public async Task<ActionResult<MoviesPagedListModel>> GetMoviesPage(int page)
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

    }
}
