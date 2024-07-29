using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainService.Controllers;

// TODO: Transfer to another project
//[AllowAnonymous]
[Authorize]
public class ExamController : ControllerBase
{
    private PolicedatabaseContext _context;

    public ExamController(PolicedatabaseContext context)
    {
        _context = context;
    }

    [HttpGet("connections")]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var ctx = HttpContext.User;
        return await _context.Users.ToListAsync();
    }

    [HttpGet("redirect")]
    public IActionResult GetRedirect()
    {
        // Setting the redirect location header
        Response.Headers.Location = "https://www.google.com";

        // Returning 300 status code with the redirect location
        return StatusCode(301);
    }

    [HttpGet("something")]
    public object GetSomething()
    {
        var ctx = HttpContext;
        return HttpContext;
    }
}
