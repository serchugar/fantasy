using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fantasy.Shared.Entities.Team;

[NotMapped]
public class TeamDTO
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;
    
    public string? Image { get; set; }
    
    [Required, Range(1, int.MaxValue)]
    public int CountryId { get; set; }
}