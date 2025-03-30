using SpajzManager.Api.Enums;
using SpajzManager.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Entities
{
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int StorageTypeId { get; set; }

        [ForeignKey("StorageTypeId")]
        public StorageType? Type { get; set; }

        [ForeignKey("HouseholdId")]
        public Household? Household { get; set; }
        public int HouseholdId { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public Storage(int storageTypeId)
        {
            StorageTypeId = storageTypeId;
        }
    }
}
