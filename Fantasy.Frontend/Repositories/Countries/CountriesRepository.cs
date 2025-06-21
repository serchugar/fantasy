using Fantasy.Frontend.Repositories.Base;
using Fantasy.Shared.Entities.Country;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Repositories.Countries;

public class CountriesRepository(HttpClient httpClient) : BaseHttpRepository<CountryDTO>(httpClient)
{
    public async Task<Response<IEnumerable<CountryDTO>>> GetAll() =>
        await base.GetManyAsync("api/countries");
}