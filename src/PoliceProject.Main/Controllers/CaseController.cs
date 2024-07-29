using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using MainService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace MainService.Controllers;

[Authorize(Roles = "Prosecutor, Judge")]
[Route("[controller]")]
[ApiController]
public class CaseController : ControllerBase
{
    private readonly ICaseFileService _service;
    private readonly PdfGenerator generator;
    public CaseController(ICaseFileService service, PdfGenerator pdfGenerator)
    {
        _service = service;
        generator = pdfGenerator;
    }

    [HttpGet("{caseFileId}")]
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

    [HttpGet("{caseFileId}/pdf")]
    public async Task<IActionResult> GetCaseFilePdf(int caseFileId)
    {
        // Retrieve the CaseFileModel based on the provided id
        var caseFile = await _service.GetByIdAsync(caseFileId);
        if (caseFile == null)
        {
            return NotFound();
        }

        var pdfBytes = generator.GenerateCaseFilePdf(caseFile);
        return File(pdfBytes, "application/pdf", "CaseFile.pdf");
    }

    [HttpGet("mine")] 
    public async Task<ActionResult<IEnumerable<CaseFilePreview>?>> GetMyCaseFiles()
    {
        try
        {
            var identifier = HttpContext.GetUserLocalIdentifier();
            if(!identifier.HasValue)
            {
                return Unauthorized();
            }
            var caseFilePreviews = await _service.GetAssignedCaseFiles(identifier.Value);
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
    public async Task<IActionResult> PutCaseFile(int id, [FromBody] CaseFileModel ticket)
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

    [Authorize(Roles = "Judge")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCaseFile(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
