using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Frontend.Shared;

public abstract class ComponentBaseWithLocalizer : ComponentBase
{
    [Inject]
    protected IStringLocalizer<Literals> Localizer { get; set; } = null!;
}

public abstract class LayoutComponentBaseWithLocalizer : LayoutComponentBase
{
    
    [Inject]
    protected IStringLocalizer<Literals> Localizer { get; set; } = null!;
}