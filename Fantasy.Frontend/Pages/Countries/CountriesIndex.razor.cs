using Fantasy.Frontend.Repositories.Countries;
using Fantasy.Shared.Entities.Country;
using Microsoft.AspNetCore.Components;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountriesIndex(CountriesRepository repo) : ComponentBase
{
    private IEnumerable<CountryDTO>? Countries { get; set; }

    protected  override async Task OnInitializedAsync()
    {
        Response<IEnumerable<CountryDTO>> response = await repo.GetAll();
        if (response.Code.IsSuccess()) Countries = response.Data;
    }
}