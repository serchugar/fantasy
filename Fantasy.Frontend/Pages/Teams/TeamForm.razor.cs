using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frontend.Repositories.Countries;
using Fantasy.Shared.Entities.Country;
using Fantasy.Shared.Entities.Team;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Serchugar.Base.Shared;

namespace Fantasy.Frontend.Pages.Teams;

public partial class TeamForm(CountriesRepository countriesRepo)
{
    private EditContext _editContext = null!;
    private List<CountryDTO>? _countries;
    
    protected override void OnInitialized()
    {
        _editContext = new(Team);
    }
    
    protected override async Task OnInitializedAsync()
    {
        await LoadCountries();
    }

    [EditorRequired, Parameter] public TeamDTO Team { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    
    public bool FormPostedSuccessfully { get; set; } = false;
    
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        // TODO: Ver en la parte 22 del curso
    }
    
    private async Task LoadCountries()
    {
        var result = await countriesRepo.GetAllAsync();
        if (result.Code.IsError()) 
        {
            await SweetAlertService.FireAsync(
                Localizer["Error"],
                result.ErrorMessage,
                SweetAlertIcon.Error
                );
            return;
        }

        _countries = result.Data!.ToList();
    }
    
    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        bool formWasEdited = _editContext.IsModified();

        if (!formWasEdited || FormPostedSuccessfully) return;
        
        SweetAlertResult result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = Localizer["Confirmation"],
            Text = Localizer["LeaveAndLoseChanges"],
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true
        });

        bool confirm = !string.IsNullOrWhiteSpace(result.Value);
        if (confirm) return;
        
        context.PreventNavigation();
    }
}