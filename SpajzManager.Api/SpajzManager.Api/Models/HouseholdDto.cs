namespace SpajzManager.Api.Models
{
    public class HouseholdDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfHouseholdItems
        {
            get
            {
                return Items.Count;
            }
        }       

        public ICollection<HouseholdItemDto> Items { get; set; } 
            = new List<HouseholdItemDto>();
        
    }
}
