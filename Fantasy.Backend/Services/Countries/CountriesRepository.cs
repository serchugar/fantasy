using Fantasy.Backend.Data;
using Fantasy.Shared.Entities.Country;
using Microsoft.EntityFrameworkCore;
using Serchugar.Base.Backend;
using Serchugar.Base.Shared; // My own nuget package :)

namespace Fantasy.Backend.Services.Countries;

public class CountriesRepository(AppDbContext context, string singular = "Country", string plural = "Countries") 
    : BaseRepository<CountryModel>(context, singular, plural) // Class from my package!! :D
{
    public async Task<Response<IEnumerable<CountryModel>>> GetAllAsync() =>
        await base.GetAllAsync();
    
    public async Task<Response<CountryModel>> GetByIdAsync(int id) =>
        await base.GetByIdAsync(id);

    public async Task<Response<CountryModel>> CreateAsync(CountryModel country)
    {
        Response<bool> result = await base.CheckIfEntityExistsAsync(c => EF.Functions.ILike(c.Name, country.Name));       
        if (result.Code.IsError()) return result.MapErrorResponse<CountryModel>();
        
        if (result.Data) return Response<CountryModel>.FromError(ResponseCodes.Conflict, $"{singular} already exists");

        return await base.CreateAsync(country);
    }

    public async Task<Response<CountryModel>> UpdateAsync(int id, CountryModel country)
    {
        Response<CountryModel> result = await base.GetByIdAsync(id);
        if (result.Code.IsError()) return result;
        
        CountryModel updatedCountry = result.Data!;
        updatedCountry.Name = country.Name;
        
        return await base.UpdateAsync(updatedCountry);
    }

    public async Task<Response<CountryModel>> DeleteAsync(int id)
    {
        var response = await base.GetByIdAsync(id);
        if (response.Code.IsError()) return response.MapErrorResponse<CountryModel>();
        
        // This is done because DeleteBehavior is set to Restricted. If set to Cascade, call base.DeleteAsync() directly
        // If this is omitted and DeleteBehavior is Restricted, the error message will be in the backend language,
        // so localization will not be possible, as it will return ResponseCodes Error
        if (response.Data!.TeamCount > 0)
            return Response<CountryModel>.FromError(ResponseCodes.Conflict, "Cannot delete because it has related 'Teams' data");
        
        return await base.DeleteAsync(id);
    }
}