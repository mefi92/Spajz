using SpajzManager.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpajzManager.Api.Entities
{
    public class Household
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<Item> Items { get; set; }
            = new List<Item>();

        public ICollection<Storage> Storages { get; set; }
            = new List<Storage>();

        public Household(string name) 
        {
            Name = name;
        }
    }
}
