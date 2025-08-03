using Fantasy.Backend.Data;
using Fantasy.Backend.Services.Countries;
using Fantasy.Shared.Entities.Team;
using Microsoft.EntityFrameworkCore;
using Serchugar.Base.Backend;
using Serchugar.Base.Shared;

namespace Fantasy.Backend.Services.Teams;

public class TeamsRepository(AppDbContext context, CountriesRepository countries, string singular = "Team", string plural = "Teams")
    : BaseRepository<TeamModel>(context, singular, plural)
{

    public async Task<Response<IEnumerable<TeamDTO>>> GetAllAsync() =>
        (await base.GetAllAsync(withAutoIncludes: true)).Map();
    
    public async Task<Response<TeamDTO>> GetByIdAsync(int id) =>
        (await base.GetByIdAsync(id)).Map();

    public async Task<Response<IEnumerable<TeamDTO>>> GetByCountryIdAsync(int countryId)
    {
        var result = await countries.CheckIfEntityExistsAsync(c => c.Id == countryId);
        if (result.Code.IsError()) return result.MapErrorResponse<IEnumerable<TeamDTO>>();
        
        if (!result.Data) return Response<IEnumerable<TeamDTO>>.FromError(ResponseCodes.NotFound, $"{countries.Singular} not found");
        
        return (await base.GetByFilterAsync(t => t.CountryId == countryId, withAutoIncludes: true)).Map();
    }

    // TODO: Ensure the country exists before creating a team in the frontend form. With a dropdown for countries.
    public async Task<Response<TeamDTO>> CreateAsync(TeamDTO team)
    {
        Response<bool> result = await base.CheckIfEntityExistsAsync(t => EF.Functions.ILike(t.Name, team.Name) && t.CountryId == team.Country.Id);
        if (result.Code.IsError()) return result.MapErrorResponse<TeamDTO>();
        
        if (result.Data) return Response<TeamDTO>.FromError(ResponseCodes.Conflict, $"{singular} already exists");

        return (await base.CreateAsync(team.Map())).Map();
    }

    public async Task<Response<TeamDTO>> UpdateAsync(int id, TeamDTO team)
    {
        Response<TeamModel> result = await base.GetByIdAsync(id);
        if (result.Code.IsError()) return result.MapErrorResponse<TeamDTO>();
        
        // If country id is not assigned here, the only way to update the country id is to delete and create a new team
        TeamModel updatedTeam = result.Data!;
        updatedTeam.Name = team.Name;
        
        return (await base.UpdateAsync(updatedTeam)).Map();
    }
    
    public async Task<Response<TeamDTO>> DeleteAsync(int id) =>
        (await base.DeleteAsync(id)).Map();
}