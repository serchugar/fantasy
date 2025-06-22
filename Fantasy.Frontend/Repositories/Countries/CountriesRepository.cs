using Fantasy.Shared.Entities.Country;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Repositories.Countries;

public class CountriesRepository(HttpClient httpClient) : Base.BaseHttpRepository<CountryDTO>(httpClient)
{
    public async Task<Response<IEnumerable<CountryDTO>>> GetAll() =>
        await base.GetManyAsync("api/countries");
    
    public async Task<Response<CountryDTO>> CreateAsync(CountryDTO country) =>
        await base.PostAsync("api/countries", country);
    
    public async Task<Response<CountryDTO>> UpdateAsync(int id, CountryDTO country) =>
        await base.PutAsync($"api/countries/{id}", country);
    
    public async Task<Response<CountryDTO>> DeleteAsync(int id) => 
        await base.DeleteAsync($"api/countries/{id}");
}