using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AngularApp1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrivingLicenseController : ControllerBase
    {
        private readonly PolicedatabaseContext context;

        public DrivingLicenseController(PolicedatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet("getdrivinglicense")]
        public async Task<ActionResult<DrivingLicense>> GetPersonDrivingLicense()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the user in the database
            var user = await context.Users.FindAsync(Convert.ToInt32(userId));

            // Find the driving license associated with the user
            var drivingLicense = await context.DrivingLicenses
                .FirstOrDefaultAsync(dl => dl.DriverId == user.Id);

            if (drivingLicense == null)
            {
                // Driving license not found for the user
                return NotFound();
            }

            return Ok(drivingLicense);
        }

        [Authorize(Policy = "RequirePolicePosition")]
        [HttpPost("RegisterDrivingLicense")]
        public async Task<IActionResult> RegisterDrivingLicense(DrivingLicense drivingLicense)
        {
            var Dbdrivinglicense = context.DrivingLicenses.FirstOrDefault(d => d.DriverId == drivingLicense.DriverId);

            if (Dbdrivinglicense != null)
            {
                if (Dbdrivinglicense.ExpirationDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    return Forbid();
                }
                context.DrivingLicenses.Remove(Dbdrivinglicense);
            }

            drivingLicense.IssueDate = DateOnly.FromDateTime(DateTime.Now);
            drivingLicense.ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(5));

            await context.DrivingLicenses.AddAsync(drivingLicense);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return CreatedAtAction(nameof(GetPersonDrivingLicense), drivingLicense);
        }
    }
}
