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
        SetResponse((await repo.GetAllAsync()).Map());
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TeamDTO>> GetByIdAsync(int id) =>
        SetResponse((await repo.GetByIdAsync(id)).Map());
    
    [HttpPost]
    public async Task<ActionResult<TeamDTO>> CreateAsync(TeamDTO country) => 
        SetResponse((await repo.CreateAsync(country.Map())).Map());
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<TeamDTO>> UpdateAsync(int id, TeamDTO country) =>
        SetResponse((await repo.UpdateAsync(id, country.Map())).Map());
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<TeamDTO>> DeleteAsync(int id) =>
        SetResponse((await repo.DeleteAsync(id)).Map());
}