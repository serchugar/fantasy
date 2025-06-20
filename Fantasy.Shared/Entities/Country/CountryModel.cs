using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Serchugar.Base.Shared;

namespace Fantasy.Shared.Entities.Country;

[Table("countries", Schema = "fantasy")]
[Index(nameof(Name), IsUnique = true)]
public class CountryModel : IPrimarykey
{
    
    [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    object IPrimarykey.Id => Id;

    [Column("name"), Required, MaxLength(64)]
    public string Name { get; set; } = null!;
}