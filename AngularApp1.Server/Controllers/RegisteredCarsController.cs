using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Authorization;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredCarsController : ControllerBase
    {
        private readonly PolicedatabaseContext _context;

        public RegisteredCarsController(PolicedatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RegisteredCars
        [Authorize(Policy = "RequirePoliceAdminPosition")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisteredCar>>> GetRegisteredCars()
        {
            return await _context.RegisteredCars.ToListAsync();
        }

        // GET: api/RegisteredCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisteredCar>> GetRegisteredCar(int id)
        {
            var registeredCar = await _context.RegisteredCars.FindAsync(id);

            if (registeredCar == null)
            {
                return NotFound();
            }

            return registeredCar;
        }

        // POST: api/RegisteredCars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("registercar")]
        public async Task<ActionResult<RegisteredCar>> PostRegisteredCar(RegisteredCar registeredCar)
        {
            _context.RegisteredCars.Add(registeredCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegisteredCar", registeredCar);
        }

        // DELETE: api/RegisteredCars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegisteredCar(int id)
        {
            var registeredCar = await _context.RegisteredCars.FindAsync(id);
            if (registeredCar == null)
            {
                return NotFound();
            }

            _context.RegisteredCars.Remove(registeredCar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegisteredCarExists(int id)
        {
            return _context.RegisteredCars.Any(e => e.Id == id);
        }
    }
}
