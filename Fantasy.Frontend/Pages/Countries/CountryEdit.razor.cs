using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frontend.Repositories.Countries;
using Fantasy.Shared.Entities.Country;
using Microsoft.AspNetCore.Components;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountryEdit(CountriesRepository repo)
{
    private CountryForm? countryForm;
    private CountryDTO? _country;
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    
    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await repo.GetByIdAsync(Id);
        
        if (responseHttp.Code == ResponseCodes.NotFound)
            NavigationManager.NavigateTo("countries");
        
        else if (responseHttp.Code.IsError())
            await SweetAlertService.FireAsync(Localizer["Error"], responseHttp.ErrorMessage, SweetAlertIcon.Error);
        
        _country = responseHttp.Data;
    }

    private async Task EditAsync()
    {
        var responseHttp = await repo.UpdateAsync(Id, _country);
        if (responseHttp.Code.IsError())
        {
            await SweetAlertService.FireAsync(Localizer["Error"], responseHttp.ErrorMessage, SweetAlertIcon.Error);
            return;
        }

        Return();
        SweetAlertMixin toast = SweetAlertService.Mixin(new SweetAlertOptions 
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            ConfirmButtonText = Localizer["Ok"],
            Timer = 3000
        });
        await toast.FireAsync(icon: SweetAlertIcon.Success, message:Localizer["RecordSavedOk"]);
    }

    private void Return()
    {
        countryForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("countries");
    }
}