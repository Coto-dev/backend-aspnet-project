using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : Controller
    {
        private IUserService _userservice;
        private ILogger<UserController> _logger;

        public UserController(IUserService userservice, ILogger<UserController> logger)
        {
            _userservice = userservice;
            _logger = logger;
        }
        [HttpGet]
        [Authorize]
        [Route("profile")]

        public async Task<ActionResult<ProfileModel>> GetProfile(){


            var TokenIsValid = await _userservice.CheckToken(Request);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
                {
                    var profile = _userservice.GetProfile(User.Identity.Name);
                    if (profile != null)
                    return Ok(profile);
                       else return BadRequest(new { errorText = "null profile" });

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
        [Authorize]
        [Route("profile")]
        public async Task<ActionResult>EditProfile([FromBody] ProfileModel modelDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var TokenIsValid = await _userservice.CheckToken(Request);
            if (!TokenIsValid)
                return BadRequest("невалидный токен");
            try
                {
                       await _userservice.EditProfile(modelDto);
                        return Ok();
                   // else return BadRequest(new { errorText = "null profile" });

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
