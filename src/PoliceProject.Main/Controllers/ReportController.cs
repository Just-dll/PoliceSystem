﻿using BLL.Interfaces;
using BLL.Models;
using MainService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MainService.Controllers;

[Authorize(Roles = "Prosecutor, Policeman, Judge")]
[Route("[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IService<ReportModel> _service;
    public ReportController(IService<ReportModel> service)
    {
        _service = service;
    }
    // GET: api/Report

    // GET api/Report/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportModel?>> Get(int id)
    {
        try
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // POST api/Report
    [Authorize(Roles = "Policeman, Prosecutor")]
    [HttpPost]
    public async Task<ActionResult<ReportModel>> PostCaseFile(int? caseFileId, ReportModel model)
    {
        try
        {
            var identifier = HttpContext.GetUserLocalIdentifier();
            if (!identifier.HasValue)
            {
                return Unauthorized();
            }
            model.ReporterId = identifier.Value;
            model.CaseFileId = caseFileId;
            await _service.AddAsync(model);
            return CreatedAtAction(nameof(PostCaseFile), model);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // PUT api/Report/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {

    }

    // DELETE api/Report/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
