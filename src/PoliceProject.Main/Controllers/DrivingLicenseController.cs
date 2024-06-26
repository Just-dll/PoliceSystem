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

        public DrivingLicenseController(IDrivingLicenseService service)
        {
            this.drivingLicenseService = service;
        }

        [HttpGet("getmydrivinglicense")]
        public async Task<ActionResult<DrivingLicense>> GetMyDrivingLicense()
        {
            // Find the driving license associated with the user
            var drivingLicense = await drivingLicenseService.GetPersonDrivingLicense(HttpContext.User.GetPrincipalIdentifier());

            if (drivingLicense == null)
            {
                // Driving license not found for the user
                return NotFound();
            }

            return Ok(drivingLicense);
        }

        [Authorize(Roles = "Policeman")]
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

        [Authorize(Roles = "Policeman")]
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
