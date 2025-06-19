using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.Entities.Country;

public class CountryDTO
{
    public int Id { get; set; }

    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;
}