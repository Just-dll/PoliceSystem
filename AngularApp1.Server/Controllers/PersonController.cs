﻿using AngularApp1.Server.Data;
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
using Microsoft.AspNetCore.Identity.Data;
using Google.Apis.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AngularApp1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly UserManager<User> manager;

        public PersonController(UserManager<User> manager)
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

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Roles = "Prosecutor, Policeman, Judge")]
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

    }
}
