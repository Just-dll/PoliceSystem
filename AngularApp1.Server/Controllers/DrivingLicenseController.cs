using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using BLL.Interfaces;
using BLL.Services;
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
        private readonly IDrivingLicenseService drivingLicenseService;
        private readonly UserManager<User> userManager;

        public DrivingLicenseController(PolicedatabaseContext context, IDrivingLicenseService service, UserManager<User> userManager)
        {
            this.context = context;
            this.drivingLicenseService = service;
            this.userManager = userManager;
        }

        [HttpGet("getmydrivinglicense")]
        public async Task<ActionResult<DrivingLicense>> GetPersonDrivingLicense()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            // Find the driving license associated with the user
            var drivingLicense = await drivingLicenseService.GetPersonDrivingLicence(user.Id);

            if (drivingLicense == null)
            {
                // Driving license not found for the user
                return NotFound();
            }

            return Ok(drivingLicense);
        }

        [Authorize(Policy = "RequirePolicePosition")]
        [HttpPost("issueDrivingLicense")]
        public async Task<IActionResult> IssueDrivingLicense(User user)
        {
            var Dbdrivinglicense = context.DrivingLicenses.SingleOrDefault(d => d.DriverId == user.Id);
            if(Dbdrivinglicense != null)
            {
                if (Dbdrivinglicense.ExpirationDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    return Forbid();
                }
                context.DrivingLicenses.Remove(Dbdrivinglicense);
            }

            var drivingLicense = new DrivingLicense
            {
                IssueDate = DateOnly.FromDateTime(DateTime.Now),
                ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(5)),
                DriverId = user.Id,
                Driver = user
            };

            user.DrivingLicense = drivingLicense;
            context.Users.Update(user);
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
