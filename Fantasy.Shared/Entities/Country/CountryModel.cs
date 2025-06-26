using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fantasy.Shared.Entities.Team;

namespace Fantasy.Shared.Entities.Country;

[Table("countries", Schema = "fantasy")]
public class CountryModel
{
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name"), Required, MaxLength(64)]
    public string Name { get; set; } = null!;
    
    
    // Nav props
    public virtual ICollection<TeamModel> Teams { get; set; } = new List<TeamModel>();
    
    // Readonly props
    public int TeamCount => Teams.Count;
}