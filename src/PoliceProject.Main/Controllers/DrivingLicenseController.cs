using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using MainService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MainService.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class DrivingLicenseController : ControllerBase
{
    private readonly IDrivingLicenseService drivingLicenseService;

    public DrivingLicenseController(IDrivingLicenseService service)
    {
        drivingLicenseService = service;
    }

    [HttpGet("user/my")]
    public async Task<ActionResult<DrivingLicense>> GetMyDrivingLicense()
    {
        // Find the driving license associated with the user
        var identifier = HttpContext.GetUserLocalIdentifier();
        if (!identifier.HasValue)
        {
            return Unauthorized();
        }
        var drivingLicense = await drivingLicenseService.GetPersonDrivingLicense(identifier.Value);

        if (drivingLicense == null)
        {
            // Driving license not found for the user
            return NotFound();
        }

        return Ok(drivingLicense);
    }

    [Authorize(Roles = "Policeman")]
    [HttpGet("user")]
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
    [HttpPost]
    public async Task<IActionResult> PostDrivingLicense(DrivingLicenseModel model)
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

    [Authorize(Roles = "Policeman")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDrivingLicense(int id, [FromBody] DrivingLicenseModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != model.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var existingLicense = await drivingLicenseService.GetPersonDrivingLicense(id);
            if (existingLicense == null)
            {
                return NotFound();
            }

            await drivingLicenseService.UpdateAsync(model);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "Judge")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDrivingLicense(int id)
    {
        try
        {
            var existingLicense = await drivingLicenseService.GetPersonDrivingLicense(id);
            if (existingLicense == null)
            {
                return NotFound();
            }

            await drivingLicenseService.DeleteAsync(existingLicense);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
