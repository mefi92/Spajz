using SpajzManager.Api.Entities;
using SpajzManager.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Models
{
    public class StorageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
