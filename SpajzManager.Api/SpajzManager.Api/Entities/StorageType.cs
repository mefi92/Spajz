using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Entities
{
    public class StorageType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
