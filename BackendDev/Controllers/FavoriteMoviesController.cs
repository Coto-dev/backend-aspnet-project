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
        public async Task <ActionResult<MoviesListModel>> GetMoviesList()
        {
            var TokenIsValid = await _favoriteMoviesService.CheckToken(Request);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
            {
                return Ok(_favoriteMoviesService.GetMoviesList(User.Identity.Name));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        [Route("{id}/add")]
        public async Task<IActionResult> AddFavorite(Guid id)
        {
            var TokenIsValid = await _favoriteMoviesService.CheckToken(Request);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
            {
                await _favoriteMoviesService.AddFavorite(User.Identity.Name, id);
                return Ok();
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
        [HttpDelete]
        [Authorize]
        [Route("{id}/delete")]
        public async Task<IActionResult> DeleteFavorite(Guid id)
        {
            var TokenIsValid = await _favoriteMoviesService.CheckToken(Request);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
            {
               await _favoriteMoviesService.DeleteFavorite(User.Identity.Name, id);
                return Ok();
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
    }
}
