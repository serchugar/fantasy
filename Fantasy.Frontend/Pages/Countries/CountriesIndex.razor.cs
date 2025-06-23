using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frontend.Repositories.Countries;
using Fantasy.Shared.Entities.Country;
using Microsoft.AspNetCore.Components;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountriesIndex(CountriesRepository repo)
{
    private IEnumerable<CountryDTO>? Countries { get; set; }
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    protected  override async Task OnInitializedAsync() =>
        await LoadAsync();

    private async Task DeleteAsync(CountryDTO country)
    {
        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = @Localizer["Confirmation"],
            Text = string.Format(Localizer["DeleteConfirm"], @Localizer["Country"], country.Name),
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            CancelButtonText = Localizer["Cancel"]
        });

        bool confirm = string.IsNullOrWhiteSpace(result.Value);
        if (confirm) return;
        
        var responseHttp = await repo.DeleteAsync(country.Id);
        
        if (responseHttp.Code == ResponseCodes.NotFound)
            NavigationManager.NavigateTo("/");
        
        else if (responseHttp.Code.IsError())
        {
            await SweetAlertService.FireAsync(Localizer["Error"], responseHttp.ErrorMessage, SweetAlertIcon.Error);
            return;
        }

        await LoadAsync();
        SweetAlertMixin toast = SweetAlertService.Mixin(new SweetAlertOptions 
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            ConfirmButtonText = Localizer["Ok"],
            Timer = 3000
        });
        // This method needs to be fired synchronously so it does not prevent the result from LoadAsync() to load until this one finishes
        toast.FireAsync(icon: SweetAlertIcon.Success, message:Localizer["RecordDeletedOk"]);
    }

    private async Task LoadAsync()
    {
        var response = await repo.GetAllAsync();
        if (response.Code.IsSuccess()) Countries = response.Data;
    }
}