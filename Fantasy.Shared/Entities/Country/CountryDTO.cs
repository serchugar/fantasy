using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fantasy.Shared.Entities.Country;

[NotMapped]
public class CountryDTO
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;
}