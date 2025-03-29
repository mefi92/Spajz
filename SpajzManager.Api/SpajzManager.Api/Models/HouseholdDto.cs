namespace SpajzManager.Api.Models
{
    public class HouseholdDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<StorageDto> Storages { get; set; }
            = new List<StorageDto>();

        public ICollection<ItemDto> Items { get; set; } 
            = new List<ItemDto>();        
    }
}
