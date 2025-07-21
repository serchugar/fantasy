using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fantasy.Shared.Resources;

namespace Fantasy.Shared.Entities.Team;

[NotMapped]
public class TeamDTO
{
    [Key]
    public int Id { get; set; }
    
    [Display(Name = "Team", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    [MaxLength(64, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;
    
    public string? Image { get; set; }
    
    [Required, Range(1, int.MaxValue)]
    public int CountryId { get; set; }
}