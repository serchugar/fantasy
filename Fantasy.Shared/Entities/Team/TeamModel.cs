using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fantasy.Shared.Entities.Country;

namespace Fantasy.Shared.Entities.Team;

[Table("teams", Schema = "fantasy")]
public class TeamModel
{
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("name"), Required, MaxLength(64)]
    public string Name { get; set; } = null!;
    
    [Column("image")]
    public string? Image { get; set; }
    
    [Column("country_id"), Required]
    public int CountryId { get; set; }
    
    
    // Nav props
    public virtual CountryModel? Country { get; set; }
    
}