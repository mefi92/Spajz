﻿using SpajzManager.Api.Enums;
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

        [ForeignKey("StorageId")]
        public Storage? Storage { get; set; }
        public int? StorageId { get; set; }

        [Required]
        public QuantityUnit Unit { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public decimal Quantity { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        

        public Item(string name, 
            decimal quantity = 1,
            QuantityUnit unit = QuantityUnit.Piece)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }
    } 
}
