using SpajzManager.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Models
{
    public class ItemForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "You should provide a unit value.")]
        public QuantityUnit Unit { get; set; } = QuantityUnit.Piece;

        [Required(ErrorMessage = "Quantity must be greater than zero.")]
        public decimal Quantity { get; set; } = 1;
    }
}
