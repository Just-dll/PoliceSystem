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
using Microsoft.AspNetCore.Authentication;
using BLL.Services;
using BLL.Models;
using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace AngularApp1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly PolicedatabaseContext _context;
        private readonly ITicketService ticketService;
        private readonly UserManager<User> userManager;
        public TicketsController(PolicedatabaseContext context, ITicketService service, UserManager<User> manager)
        {
            _context = context;
            ticketService = service;
            userManager = manager;
        }
        //[HttpGet("myTickets")]
        //public async Task<ActionResult<IEnumerable<TicketModel>>> GetMyTickets()
        //{
        //    try
        //    {
        //        var user = userManager.GetUserAsync(HttpContext.User);
        //        var tickets = await ticketService.GetPersonTicketsAsync(user.Id);
        //        if (tickets == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(tickets);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet("personTickets")]
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetPersonTickets(int? personId)
        {
            try
            {
                if (personId == null)
                {
                    var user = await userManager.GetUserAsync(HttpContext.User);
                    personId = user.Id;
                }
                var tickets = await ticketService.GetPersonTicketsAsync(personId.Value);
                if (tickets == null)
                {
                    return NotFound();
                }
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        //// GET: api/Tickets
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        //{
        //    return await _context.Tickets.ToListAsync();
        //}

        //// GET: api/Tickets/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Ticket>> GetTicket(int id)
        //{
        //    var ticket = await _context.Tickets.FindAsync(id);

        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    return ticket;
        //}

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "RequirePolicePosition")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("issueTicket")]
        [Authorize(Policy = "RequirePolicePosition")]
        public async Task<ActionResult<Ticket>> PostTicket(TicketModel ticket)
        {
            try
            {
                await ticketService.AddAsync(ticket);

                return CreatedAtAction(nameof(GetPersonTickets), ticket);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequirePolicePosition")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
