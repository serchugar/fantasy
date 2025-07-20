using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fantasy.Shared.Entities.Team;

[NotMapped]
public class TeamDTO
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public int CountryId { get; set; }
}