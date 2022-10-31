using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendDev.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoriteMoviesController : Controller
    {
        private IFavoriteMoviesService _favoriteMoviesService;
        private ILogger<FavoriteMoviesController> _logger;
        public FavoriteMoviesController(IFavoriteMoviesService favoriteMoviesService, ILogger<FavoriteMoviesController> logger)
        {
            _favoriteMoviesService = favoriteMoviesService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<MoviesListModel> GetMoviesList()
        {
            try
            {
                return Ok(_favoriteMoviesService.GetMoviesList(User.Identity.Name));
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during getting MoviesListModel");
            }
        }
        [HttpPost]
        [Authorize]
        [Route("{id}/add")]
        public async Task<IActionResult> AddFavourite(Guid id)
        {
            try
            {
                await _favoriteMoviesService.AddFavorite(User.Identity.Name, id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during adding Favorite Film");
            }
        }
        [HttpPost]
        [Authorize]
        [Route("{id}/delete")]
        public async Task<IActionResult> DeleteFavorite(Guid id)
        {
            try
            {
               await _favoriteMoviesService.DeleteFavorite(User.Identity.Name, id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during deleting Favorite Film");
            }
        }
    }
}
