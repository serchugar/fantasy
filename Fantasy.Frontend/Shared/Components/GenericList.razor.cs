using Microsoft.AspNetCore.Components;

namespace Fantasy.Frontend.Shared.Components;

public partial class GenericList<T>
{
    [Parameter] public RenderFragment? Loading { get; set; }
    
    [Parameter] public RenderFragment? NoRecords { get; set; }
    
    [EditorRequired, Parameter] public RenderFragment Body { get; set; } = null!;
    
    [EditorRequired, Parameter] public IEnumerable<T>? MyList { get; set; }
}