using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainService.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class ConnectionController : ControllerBase
{
    private readonly IAssignationService assignationService;
    public ConnectionController(IAssignationService assignationService)
    {
        this.assignationService = assignationService;
    }

    [Authorize(Roles = "Judge, Prosecutor")]
    [HttpPost]
    public async Task<IActionResult> ConnectPerson(int personId, int caseFileId, int connectionType = 1)
    {
        CaseFileConnectionModel connection = await assignationService.Assign(caseFileId, personId, connectionType);
        return CreatedAtAction(nameof(ConnectPerson), connection);
    }

    //[Authorize(Roles = "Judge, Prosecutor")]
    //[HttpPut]
    //public async Task<IActionResult> PutConnection(int id, [FromBody] CaseFileConnectionModel ticket)
    //{

    //}

}
