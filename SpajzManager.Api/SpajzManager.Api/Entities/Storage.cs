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
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("HouseholdId")]
        public Household? Household { get; set; }
        public int HouseholdId { get; set; }

        public Storage(string name)
        {
            Name = name;
        }
    }
}
