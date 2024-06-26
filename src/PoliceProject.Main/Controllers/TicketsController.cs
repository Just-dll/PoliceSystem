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
using Hangfire;

namespace AngularApp1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService ticketService;
        public TicketsController(ITicketService service)
        {
            ticketService = service;
        }

        [HttpGet("myTickets")]
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetMyTickets()
        {
            try
            {
                var tickets = await ticketService.GetPersonTicketsAsync(HttpContext.User.GetPrincipalIdentifier());
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

        [Authorize(Roles = "Policeman, Prosecutor, Judge")]
        [HttpGet("personTickets")]
        public async Task<ActionResult<IEnumerable<PersonTicketModel>>> GetPersonTickets([FromQuery] int id)
        {
            try
            {
                var tickets = await ticketService.GetPersonTicketsAsync(id);
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

        // GET: api/Tickets/5
        [Authorize(Roles = "Policeman, Prosecutor, Judge")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketModel>> GetTicket(int id)
        {
            var ticket = await ticketService.GetByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Policeman, Prosecutor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, TicketModel ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }
            try
            {
                await ticketService.UpdateAsync(ticket);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                if((await ticketService.GetByIdAsync(id)) == null)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Policeman")]
        [HttpPost("issueTicket")]
        public async Task<ActionResult<Ticket>> PostTicket(TicketModel ticket)
        {
            try
            {
                await ticketService.AddAsync(ticket, HttpContext.User.GetPrincipalIdentifier());

                return CreatedAtAction(nameof(GetPersonTickets), ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Tickets/5
        [Authorize(Roles = "Policeman, Prosecutor, Judge")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await ticketService.GetByIdAsync(id);

            if (ticket == null)
            {
                return NotFound(); 
            }

            await ticketService.RemoveAsync(ticket);

            return NoContent();
        }

        [HttpDelete("payFine/{id}")]
        public async Task<IActionResult> PayFine(int id)
        {
            return NoContent();
        }
    }
}
