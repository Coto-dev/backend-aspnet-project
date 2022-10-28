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
        public async Task<ActionResult<MovieDetailsModel>>GetMvoieDetails(string Id)
        {
            return Ok();
        }
        [HttpGet]
        [Route("{page}")]
        public async Task<ActionResult<MovieDetailsModel>> GetMoviesPage(int page)
        {
            return Ok();
        }

    }
}
