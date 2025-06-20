using System.ComponentModel.DataAnnotations;
using Serchugar.Base.Shared;

namespace Fantasy.Shared.Entities.Country;

public class CountryDTO : IPrimarykey
{
    public int Id { get; set; }
    object IPrimarykey.Id => Id;

    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;
}