using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fantasy.Shared.Resources;

namespace Fantasy.Shared.Entities.Country;

[NotMapped]
public class CountryDTO
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Country", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    [MaxLength(64, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;
    
    public int TeamCount { get; set; }
}