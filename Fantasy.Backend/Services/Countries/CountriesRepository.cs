using Fantasy.Backend.Data;
using Fantasy.Shared.Entities.Country;
using Microsoft.EntityFrameworkCore;
using Serchugar.Base.Backend; // My own nuget package :)

namespace Fantasy.Backend.Services.Countries;

public class CountriesRepository(AppDbContext context, string singular = "Country", string plural = "Countries") 
    : BaseRepository<CountryModel>(context, singular, plural) // Class from my package!! :D
{
    public async Task<Response<IEnumerable<CountryDTO>>> GetAllAsync() =>
        (await base.GetAllAsync()).Map();
    
    public async Task<Response<CountryDTO>> GetByIdAsync(int id) =>
        (await base.GetByIdAsync(id)).Map();

    public async Task<Response<CountryDTO>> CreateAsync(CountryDTO country)
    {
        Response<bool> result = await base.CheckIfEntityExistsAsync(c => EF.Functions.ILike(c.Name, country.Name));       
        if (result.Code.IsError()) return result.MapErrorResponse<CountryDTO>();
        
        if (result.Data) return Response<CountryDTO>.FromError(ResponseCodes.Conflict, $"{singular} already exists");

        return (await base.CreateAsync(country.Map())).Map();
    }

    public async Task<Response<CountryDTO>> UpdateAsync(int id, CountryDTO country)
    {
        Response<CountryModel> result = await base.GetByIdAsync(id);
        if (result.Code.IsError()) return result.MapErrorResponse<CountryDTO>();
        
        CountryModel updatedCountry = result.Data!;
        updatedCountry.Name = country.Name;
        
        return (await base.UpdateAsync(updatedCountry)).Map();
    }
    
    public async Task<Response<CountryDTO>> DeleteAsync(int id) => 
        (await base.DeleteAsync(id)).Map();
}