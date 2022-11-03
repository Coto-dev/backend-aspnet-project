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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"];
            var TokenIsValid = await _reviewService.CheckToken(token);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
                {
                    await _reviewService.AddReviewToMovie(movieId, reviewModifyModel, User.Identity.Name);
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

        [HttpPut]
        [Route("{movieId}/review/{id}/edit")]
        [Authorize]
        public async Task<IActionResult> EditReview(Guid movieId,Guid id, [FromBody] ReviewModifyModel reviewModifyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"];
            var TokenIsValid = await _reviewService.CheckToken(token);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
            {
                await _reviewService.EditReview(movieId, id, reviewModifyModel, User.Identity.Name);
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
        [Route("{movieId}/review/{id}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(Guid movieId, Guid id)
        {
            var token = Request.Headers["Authorization"];
            var TokenIsValid = await _reviewService.CheckToken(token);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
            {
                await _reviewService.DeleteReview(movieId, id, User.Identity.Name);
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
