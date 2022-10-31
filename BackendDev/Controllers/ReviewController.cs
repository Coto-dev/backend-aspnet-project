using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewService _reviewService;
        private ILogger<ReviewController> _logger;
        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        [HttpPost]
        [Route("{movieId}/review/add")]
        [Authorize]
        public async Task<IActionResult> AddReviewToMovie(Guid movieId, [FromBody]ReviewModifyModel reviewModifyModel)
        {
            try
            {
                await _reviewService.AddReviewToMovie(movieId, reviewModifyModel, User.Identity.Name);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors during adding review");
            }
         
        }

        [HttpPut]
        [Route("{movieId}/review/{id}/edit")]
        [Authorize]
        public async Task<IActionResult> EditReview(Guid movieId,Guid id, [FromBody] ReviewModifyModel reviewModifyModel)
        {
            try
            {
                await _reviewService.EditReview(movieId, id, reviewModifyModel, User.Identity.Name);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors editting review");
            }

        }

        [HttpDelete]
        [Route("{movieId}/review/{id}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(Guid movieId, Guid id)
        {
            try
            {
                await _reviewService.DeleteReview(movieId, id, User.Identity.Name);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Errors deleting review");
            }

        }

    }
}
