using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using BLL.Services;
using BLL.Models;
using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using Hangfire;
using DAL.Entities;
using MainService.Extensions;

namespace MainService.Controllers;

[Authorize]
[Route("[controller]")]
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
            var localIdentifier = HttpContext.GetUserLocalIdentifier();
            if (!localIdentifier.HasValue)
            {
                return Unauthorized();
            }

            var tickets = await ticketService.GetPersonTicketsAsync(localIdentifier.Value);
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

    [HttpGet("user")]
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
        catch (DbUpdateConcurrencyException ex)
        {
            if (await ticketService.GetByIdAsync(id) == null)
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
    [HttpPost]
    public async Task<ActionResult<Ticket>> PostTicket(TicketModel ticket)
    {
        try
        {
            var identifier = HttpContext.GetUserLocalIdentifier();
            if (!identifier.HasValue)
            {
                return Unauthorized();
            }

            await ticketService.AddAsync(ticket, identifier.Value);

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
