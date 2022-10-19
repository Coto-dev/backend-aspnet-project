using BackendDev.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDev.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AuthController : Controller
    {

        private ContextDataBase db;
        public AuthController(ContextDataBase context)
        {
            db = context;
        }

        /*  public async Task<IActionResult> Index()
          {
              return View(await db.Users.ToListAsync());
          }*/
      /*  public IActionResult Create()
        {
            return View();
        }*/
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Create(UserModel user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok("good");
        }
    }
}
