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
}