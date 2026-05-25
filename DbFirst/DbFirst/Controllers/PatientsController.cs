using DbFirst.DTOs;
using DbFirst.Exceptions;
using DbFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbFirst.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients(string? search)
    {
        var result = await _dbService.GetPatients(search);
        return Ok(result);
    }

    [HttpPost]
    [Route("{pesel}/bedassigments")]
    public async Task<IActionResult> AssignBedForPatient(string pesel, PostBedAssigmentDto data)
    {
        try
        {
            await _dbService.AssignBedForPatient(pesel, data);
            return NoContent();   
        }catch(NoBedsAvailableException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (PatientNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }
}