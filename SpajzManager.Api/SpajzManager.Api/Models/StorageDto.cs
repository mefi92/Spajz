using SpajzManager.Api.Entities;
using SpajzManager.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpajzManager.Api.Models
{
    public class StorageDto
    {
        public int Id { get; set; }
        public int StorageTypeId { get; set; }
        public string? Description { get; set; }
    }
}
