using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frontend.Repositories.Teams;
using Fantasy.Shared.Entities.Team;
using Microsoft.AspNetCore.Components;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Pages.Teams;

public partial class TeamCreate(TeamsRepository repo)
{
    private TeamForm? _teamForm;
    private TeamDTO _team = new();

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private async Task CreateAsync()
    {
        var responseHttp = await repo.CreateAsync(_team);
        if (responseHttp.Code.IsError())
        {
            await SweetAlertService.FireAsync(Localizer["Error"], Localizer[$"{responseHttp.ErrorMessage}"], SweetAlertIcon.Error);
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
        await toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RecordCreatedOk"]);
    }

    private void Return()
    {
        _teamForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/teams");
    }
}