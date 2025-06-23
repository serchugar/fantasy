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
    
    public async Task<Response<CountryDTO>> CreateAsync(CountryDTO country) =>
        await base.PostAsync(BaseUrl, country);
    
    public async Task<Response<CountryDTO>> UpdateAsync(int id, CountryDTO country) =>
        await base.PutAsync(BaseUrl + id, country);
    
    public async Task<Response<CountryDTO>> DeleteAsync(int id) => 
        await base.DeleteAsync(BaseUrl + id);
}