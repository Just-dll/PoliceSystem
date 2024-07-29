using IdentityService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityService.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class PolicePositionsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    public PolicePositionsController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("getMyPositions")]
    public async Task<IEnumerable<string>> GetMyPositions()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        var roles = await _userManager.GetRolesAsync(user);

        return roles;
    }

    [Authorize(Policy = "RequirePoliceAdminPosition")]
    [HttpPatch]
    public async Task<ActionResult> PolicePromotion(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return NotFound();
        }

        var isPolicemanRole = await _userManager.IsInRoleAsync(user, "Policeman");

        if (isPolicemanRole)
        {
            return BadRequest("User already has the Policeman role.");
        }

        // AddAsync the Policeman role to the user
        var result = await _userManager.AddToRoleAsync(user, "Policeman");

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User promoted to Policeman role successfully.");


    }

    //[Authorize(Policy = "RequirePoliceAdminPosition")]
    [HttpPatch("prosecutor_promition")]
    public async Task<ActionResult> ProsecutorPromotion(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return NotFound();
        }

        var isPolicemanRole = await _userManager.IsInRoleAsync(user, "Prosecutor");

        if (isPolicemanRole)
        {
            return BadRequest("User already has the Prosecutor role.");
        }

        // AddAsync the Policeman role to the user
        var result = await _userManager.AddToRoleAsync(user, "Prosecutor");

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User promoted to Prosecutor role successfully.");


    }
}
