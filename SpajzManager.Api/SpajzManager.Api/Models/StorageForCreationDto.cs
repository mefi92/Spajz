using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Models
{
    public class StorageForCreationDto
    {
        [Required(ErrorMessage = "You should provide a storage type ID.")]
        public int StorageTypeId { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
