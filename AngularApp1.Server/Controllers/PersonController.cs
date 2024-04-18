using AngularApp1.Server.Data;
using AngularApp1.Server.Extensions;
using AngularApp1.Server.Models;
using BLL.Models;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuzzySharp;

namespace AngularApp1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly UserManager<User> manager;

        public PersonController(UserManager<User> manager, PolicedatabaseContext context)
        {
            this.manager = manager;
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

        //[HttpPatch("patchUser")]
        //public async Task<IActionResult> addPersonalData()
        //{
        //    var user = await manager.GetUserAsync(HttpContext.User);
        //    return Ok();
        //}

        [HttpGet("ShareMyself")]
        public async Task<ActionResult<string?>> shareMyself()
        {
            var user = await manager.GetUserAsync(HttpContext.User);
#pragma warning disable CS8602
            return $"https://localhost:7265/api/Person/getUser?id={user.Id}";
#pragma warning restore CS8602
        }
        [Authorize(Policy = "RequirePolicePosition")]
        [HttpGet("getUser")]
        public async Task<ActionResult<User>> getPerson([FromQuery] int id)
        {
            var user = await manager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("SearchByQuery")]
        public async Task<ActionResult<IList<UserSearchModel>>> GetUsersByQuery([FromQuery] string query)
        {
            var users = await manager.Users.ToListAsync();

            query = query.ToLower();

            var filteredUsers = users
                .Where(u => Fuzz.PartialTokenSetRatio(u.UserName.ToLower(), query) > 70)
                .Select(u => new UserSearchModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = u.UserName,
                })
                .ToList();  

            return filteredUsers;
        }

        private double CalcLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s) && string.IsNullOrEmpty(t))
            {
                return 0;
            }

            if (string.IsNullOrEmpty(s))
            {
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            var n = s.Length;
            var m = t.Length;
            var matrix = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                matrix[0, j] = j;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1];
                    }
                    else
                    {
                        matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j - 1], matrix[i - 1, j]), matrix[i, j - 1]) + 1;
                    }
                }
            }
            var maxlen = (double)Math.Max(m, n);
            double sim = matrix[n, m] / maxlen;
            return sim;
        }
    }
}
