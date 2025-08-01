using Fantasy.Backend.Data;
using Fantasy.Shared.Entities.Team;
using Microsoft.EntityFrameworkCore;
using Serchugar.Base.Backend;
using Serchugar.Base.Shared;

namespace Fantasy.Backend.Services.Teams;

public class TeamsRepository(AppDbContext context, string singular = "Team", string plural = "Teams")
    : BaseRepository<TeamModel>(context, singular, plural)
{
    public async Task<Response<IEnumerable<TeamModel>>> GetAllAsync() =>
        await base.GetAllAsync();
    
    public async Task<Response<TeamModel>> GetByIdAsync(int id) =>
        await base.GetByIdAsync(id);

    public async Task<Response<TeamModel>> CreateAsync(TeamModel team)
    {
        Response<bool> result = await base.CheckIfEntityExistsAsync(t => EF.Functions.ILike(t.Name, team.Name) && t.CountryId == team.CountryId);
        if (result.Code.IsError()) return result.MapErrorResponse<TeamModel>();
        
        if (result.Data) return Response<TeamModel>.FromError(ResponseCodes.Conflict, $"{singular} already exists");

        return await base.CreateAsync(team);
    }

    public async Task<Response<TeamModel>> UpdateAsync(int id, TeamModel team)
    {
        Response<TeamModel> result = await base.GetByIdAsync(id);
        if (result.Code.IsError()) return result;
        
        TeamModel updatedTeam = result.Data!;
        updatedTeam.Name = team.Name;
        
        return await base.UpdateAsync(updatedTeam);
    }
    
    public async Task<Response<TeamModel>> DeleteAsync(int id) =>
        await base.DeleteAsync(id);
}