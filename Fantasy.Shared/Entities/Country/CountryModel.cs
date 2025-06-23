using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fantasy.Shared.Entities.Team;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Shared.Entities.Country;

[Table("countries", Schema = "fantasy")]
[Index(nameof(Name), IsUnique = true)]
public class CountryModel
{
    
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name"), Required, MaxLength(64)]
    public string Name { get; set; } = null!;
    
    // Navigation properties
    //public virtual ICollection<TeamModel>? Teams { get; set; }
    
    // Readonly property. These are not mapped to the database when migrations with code-first
    //public virtual int TeamsCount => Teams?.Count ?? 0;
}