using Fantasy.Shared.Entities.Team;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Repositories.Teams;

public class TeamsRepository(HttpClient httpClient) : Base.BaseHttpRepository<TeamDTO>(httpClient)
{
    private const string BaseUrl = "api/teams/";
    
    public async Task<Response<IEnumerable<TeamDTO>>> GetAllAsync() =>
        await base.GetManyAsync(BaseUrl);
    
    public async Task<Response<TeamDTO>> GetByIdAsync(int id) =>
        await base.GetAsync(BaseUrl + id);

    public async Task<Response<TeamDTO>> CreateAsync(TeamDTO country)
    {
        var response = await base.PostAsync(BaseUrl, country);
        if (response.Code == ResponseCodes.Conflict) 
            return Response<TeamDTO>.FromError(response.Code, "http409_create");
        
        return response;
    }

    public async Task<Response<TeamDTO>> UpdateAsync(int id, TeamDTO country)
    {
        var response = await base.PutAsync(BaseUrl + id, country);
        if (response.Code == ResponseCodes.NotFound)
            return Response<TeamDTO>.FromError(response.Code, "http404");
        
        return response;
    }

    public async Task<Response<TeamDTO>> DeleteAsync(int id)
    {
        var response = await base.DeleteAsync(BaseUrl + id);
        if (response.Code == ResponseCodes.Conflict)   
            return Response<TeamDTO>.FromError(response.Code, "http409_del");
        
        return response;
    }
}