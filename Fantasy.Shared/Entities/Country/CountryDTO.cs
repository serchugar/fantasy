using System.ComponentModel.DataAnnotations;
using Serchugar.Base.Shared;

namespace Fantasy.Shared.Entities.Country;

public class CountryDTO : IPrimaryKey
{
    public int Id { get; set; }
    object IPrimaryKey.Id => Id;

    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;
}