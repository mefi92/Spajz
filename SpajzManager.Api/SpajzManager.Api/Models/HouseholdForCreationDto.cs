using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Models
{
    public class HouseholdForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
