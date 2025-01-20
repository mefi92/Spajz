using SpajzManager.Api.Models;

namespace SpajzManager.Api
{
    public class HouseholdItemDataStore
    {
        public List<HouseholdItemDto> HouseholdItems { get; set; }
        public static HouseholdItemDataStore Current { get; } = new HouseholdItemDataStore();

        public HouseholdItemDataStore()
        {
            HouseholdItems = new List<HouseholdItemDto>()
            {
                new HouseholdItemDto()
                {
                    Id = 1,
                    Name = "Alma",
                    Description = "gyümölcs"
                },
                new HouseholdItemDto()
                {
                    Id = 2,
                    Name = "Tej",
                    Description = "egyenesen a tehénből"
                },
                new HouseholdItemDto()
                {
                    Id = 3,
                    Name = "Brokkoli",
                    Description = "Az ultimate zöldség"
                }
            };
        }
    }
}
