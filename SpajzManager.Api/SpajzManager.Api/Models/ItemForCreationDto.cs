using SpajzManager.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Models
{
    public class ItemForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "You should provide a unit value.")]
        public QuantityUnit Unit { get; set; } = QuantityUnit.Piece;
    }
}
