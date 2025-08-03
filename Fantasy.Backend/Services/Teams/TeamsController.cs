using Fantasy.Shared.Entities.Team;
using Microsoft.AspNetCore.Mvc;
using Serchugar.Base.Backend;

namespace Fantasy.Backend.Services.Teams;

[ApiController]
[Route("api/[controller]")]
public class TeamsController(TeamsRepository repo) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAllAsync() =>
        SetResponse(await repo.GetAllAsync());
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TeamDTO>> GetByIdAsync(int id) =>
        SetResponse(await repo.GetByIdAsync(id));
    
    [HttpGet("country/{countryId:int}")]
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetByCountryIdAsync(int countryId) =>
        SetResponse(await repo.GetByCountryIdAsync(countryId));
    
    [HttpPost]
    public async Task<ActionResult<TeamDTO>> CreateAsync(TeamDTO country) => 
        SetResponse(await repo.CreateAsync(country));
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<TeamDTO>> UpdateAsync(int id, TeamDTO country) =>
        SetResponse(await repo.UpdateAsync(id, country));
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<TeamDTO>> DeleteAsync(int id) =>
        SetResponse(await repo.DeleteAsync(id));
}