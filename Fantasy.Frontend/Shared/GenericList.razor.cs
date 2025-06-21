using Microsoft.AspNetCore.Components;

namespace Fantasy.Frontend.Shared;

public partial class GenericList<T> : ComponentBase
{
    [Parameter]
    public RenderFragment? Loading { get; set; }
    
    [Parameter]
    public RenderFragment? NoRecords { get; set; }
    
    [EditorRequired, Parameter]
    public RenderFragment Body { get; set; } = null!;
    
    [EditorRequired, Parameter]
    public IEnumerable<T>? MyList { get; set; }
}