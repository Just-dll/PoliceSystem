using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using BLL.Interfaces;
using BLL.Models;
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
        private readonly IDrivingLicenseService drivingLicenseService;
        private readonly UserManager<User> userManager;

        public DrivingLicenseController(IDrivingLicenseService service, UserManager<User> userManager)
        {
            this.drivingLicenseService = service;
            this.userManager = userManager;
        }

        [HttpGet("getmydrivinglicense")]
        public async Task<ActionResult<DrivingLicense>> GetMyDrivingLicense()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            // Find the driving license associated with the user
            var drivingLicense = await drivingLicenseService.GetPersonDrivingLicense(user.Id);

            if (drivingLicense == null)
            {
                // Driving license not found for the user
                return NotFound();
            }

            return Ok(drivingLicense);
        }

        [Authorize(Policy = "RequirePolicePosition")]
        [HttpGet("GetPersonDrivingLicense")]
        public async Task<ActionResult<DrivingLicense>> GetPersonDrivingLicense([FromQuery] int id)
        {
            var drivingLicense = await drivingLicenseService.GetPersonDrivingLicense(id);

            if (drivingLicense == null)
            {
                // Driving license not found for the user
                return NotFound();
            }

            return Ok(drivingLicense);
        }

        [Authorize(Policy = "RequirePolicePosition")]
        [HttpPost("issueDrivingLicense")]
        public async Task<IActionResult> IssueDrivingLicense(DrivingLicenseModel model)
        {
            try
            {
                var Dbdrivinglicense = await drivingLicenseService.GetPersonDrivingLicense(model.DriverId);
                if (Dbdrivinglicense != null)
                {
                    if (Dbdrivinglicense.ExpirationDate > DateOnly.FromDateTime(DateTime.Now))
                    {
                        return Forbid();
                    }
                    await drivingLicenseService.DeleteAsync(Dbdrivinglicense);
                }

                await drivingLicenseService.AddAsync(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return CreatedAtAction(nameof(GetPersonDrivingLicense), model);
        }
    }
}
