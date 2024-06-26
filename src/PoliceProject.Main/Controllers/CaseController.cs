using AngularApp1.Server.Models;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace AngularApp1.Server.Controllers
{
    [Authorize(Roles = "Prosecutor, Judge")]
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly ICaseFileService _service;
        private readonly UserManager<User> _userManager;    
        public CaseController(ICaseFileService service, UserManager<User> manager)
        {
            _service = service;
            _userManager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<CaseFileModel?>> GetCaseFile(int caseFileId)
        {
            try
            {
                var casefile = await _service.GetByIdAsync(caseFileId);
                if (casefile == null)
                {
                    return NotFound();
                }
                return Ok(casefile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("mine")] 
        public async Task<ActionResult<IEnumerable<CaseFilePreview>?>> GetMyCaseFiles()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if(user == null)
                {
                    return Unauthorized();
                }
                var caseFilePreviews = await _service.GetAssignedCaseFiles(user.Id);
                if(caseFilePreviews == null)
                {
                    return NotFound();
                }
                return Ok(caseFilePreviews);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        
        }

        [HttpPost]
        public async Task<ActionResult<CaseFileModel>> PostCaseFile(CaseFileModel model)
        {
            try
            {
                await _service.AddAsync(model);
                return CreatedAtAction(nameof(PostCaseFile), model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaseFile(int id, CaseFileModel ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(ticket);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _service.GetByIdAsync(id) == null)
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
    }
}
