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
       
        public UserController(IUserService userservice)
        {
            _userservice = userservice;
        }
        [HttpGet]
        [Authorize]
        [Route("profile")]

        public async Task<ActionResult<ProfileModel>> GetProfile(){

            var TokenIsValid = await _userservice.CheckToken(Request);
            if (TokenIsValid)
            {
                try
                {
                    var profile = _userservice.GetProfile(User.Identity.Name);
                    if (profile != null)
                    return Ok(profile);
                       else return BadRequest(new { errorText = "null profile" });

                }
                catch (ArgumentException e)
                {

                    return Problem(e.Message);
                }
                catch (Exception ex)
                {

                    // TODO: Добавить логирование
                    return StatusCode(500, "Errors get profile");
                }
            }

            else return BadRequest(new { errorText = "Invalid token" });
        }
        [HttpPut]
        [Authorize]
        [Route("profile")]
        public async Task<ActionResult>EditProfile([FromBody] ProfileModel modelDto)
        {
            var TokenIsValid = await _userservice.CheckToken(Request);
            if (TokenIsValid)
            {
                try
                {
                       await _userservice.EditProfile(modelDto);
                        return Ok();
                   // else return BadRequest(new { errorText = "null profile" });

                }
                catch (ArgumentException e)
                {
                    return Problem(e.Message);
                }
                catch (Exception ex)
                {
                    // TODO: Добавить логирование
                    return StatusCode(500, "Errors edit profile");
                }
            }
            else return BadRequest(new { errorText = "Invalid token" });
        }
    }
}
