using Fantasy.Shared.Entities.Country;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Repositories.Countries;

public class CountriesRepository(HttpClient httpClient) : Base.BaseHttpRepository<CountryDTO>(httpClient)
{
    private const string BaseUrl = "api/countries/";
    
    public async Task<Response<IEnumerable<CountryDTO>>> GetAllAsync() =>
        await base.GetManyAsync(BaseUrl);
    
    public async Task<Response<CountryDTO>> GetByIdAsync(int id) =>
        await base.GetAsync(BaseUrl + id);

    public async Task<Response<CountryDTO>> CreateAsync(CountryDTO country)
    {
        var response = await base.PostAsync(BaseUrl, country);
        if (response.Code == ResponseCodes.Conflict) 
            return Response<CountryDTO>.FromError(response.Code, "http409_create");
        
        return response;
    }

    public async Task<Response<CountryDTO>> UpdateAsync(int id, CountryDTO country)
    {
        var response = await base.PutAsync(BaseUrl + id, country);
        if (response.Code == ResponseCodes.NotFound)
            return Response<CountryDTO>.FromError(response.Code, "http404");
        
        return response;
    }

    public async Task<Response<CountryDTO>> DeleteAsync(int id)
    {
        var response = await base.DeleteAsync(BaseUrl + id);
        if (response.Code == ResponseCodes.Conflict)   
            return Response<CountryDTO>.FromError(response.Code, "http409_del");
        
        return response;
    }
}