using Fantasy.Shared.Entities.Country;
using Microsoft.AspNetCore.Mvc;
using Serchugar.Base.Backend;

namespace Fantasy.Backend.Services.Countries;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(CountriesRepository repo) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDTO>>> GetAllAsync() =>
        SetResponse(await repo.GetAllAsync());
    
    [HttpGet("{id:int}", Name = RouteNames.GetById)]
    public async Task<ActionResult<CountryDTO>> GetByIdAsync(int id) =>
        SetResponse(await repo.GetByIdAsync(id));
    
    [HttpPost]
    public async Task<ActionResult<CountryDTO>> CreateAsync(CountryDTO country) => 
        SetResponse(await repo.CreateAsync(country));
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CountryDTO>> UpdateAsync(int id, CountryDTO country) =>
        SetResponse(await repo.UpdateAsync(id, country));
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CountryDTO>> DeleteAsync(int id) =>
        SetResponse(await repo.DeleteAsync(id));
}