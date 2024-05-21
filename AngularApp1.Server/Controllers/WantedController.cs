using AngularApp1.Server.Data;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace AngularApp1.Server.Controllers
{
    public class WantedController : ControllerBase
    {
        private PolicedatabaseContext _context;

        public WantedController(PolicedatabaseContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> getMostWanted()
        {
            return Array.Empty<UserModel>();
        }

        //auth for judge
        [HttpPost]
        public async Task<IActionResult> IssueWarrant(/*Casefile, suspect*/)
        {
            return Ok();
        }
    }
}
