using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Shared.Entities.Country;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountryForm
{
    private EditContext _editContext = null!;

    protected override void OnInitialized()
    {
        _editContext = new(Country);
    }

    [EditorRequired, Parameter] public CountryDTO Country { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    public bool FormPostedSuccessfully { get; set; } = false;
    
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

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