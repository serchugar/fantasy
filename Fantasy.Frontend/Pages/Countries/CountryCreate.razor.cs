using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frontend.Repositories.Countries;
using Fantasy.Shared.Entities.Country;
using Microsoft.AspNetCore.Components;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountryCreate(CountriesRepository repo)
{
    private CountryForm? _countryForm;
    private CountryDTO _country = new();
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await repo.CreateAsync(_country);
        if (responseHttp.Code.IsError())
        {
            await SweetAlertService.FireAsync(Localizer["Error"], responseHttp.ErrorMessage, SweetAlertIcon.Error);
            return;
        }

        Return(); 
        // Toast is like snackbar
        SweetAlertMixin toast = SweetAlertService.Mixin(new SweetAlertOptions 
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            ConfirmButtonText = Localizer["Ok"],
            Timer = 3000
        });
        await toast.FireAsync(icon: SweetAlertIcon.Success, message:Localizer["RecordCreatedOk"]);
    }

    private void Return()
    {
        _countryForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/countries");
    }
}