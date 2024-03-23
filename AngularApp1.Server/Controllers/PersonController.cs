using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly UserManager<User> manager;
        private readonly PolicedatabaseContext context;

        public PersonController(UserManager<User> manager, PolicedatabaseContext context)
        {
            this.manager = manager;
            this.context = context;
        }

        [HttpGet("getMyself")]
        public async Task<ActionResult<User>> getMyself()
        {
            var user = await manager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPatch("patchUser")]
        public async Task<IActionResult> addPersonalData()
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            return Ok();
        }

        [HttpGet("ShareMyself")]
        public async Task<ActionResult<string?>> shareMyself()
        {
            var user = await manager.GetUserAsync(HttpContext.User);

            return $"https://localhost:7265/api/Person/getUser?id={user.Id}";
        }
        [Authorize(Policy = "RequirePolicePosition")]
        [HttpGet("getUser")]
        public async Task<ActionResult<User>> getPerson([FromQuery] int id)
        {
            var user = await manager.FindByIdAsync(id.ToString());

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
