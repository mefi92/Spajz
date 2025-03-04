using SpajzManager.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpajzManager.Api.Entities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [ForeignKey("HouseholdId")]
        public Household? Household { get; set; }
        public int HouseholdId { get; set; }

        [Required]
        public QuantityUnit Unit { get; set; } = QuantityUnit.Piece;

        public Item(string name)
        {
            Name = name;
            Unit = QuantityUnit.Piece;
        }
    } 
}
