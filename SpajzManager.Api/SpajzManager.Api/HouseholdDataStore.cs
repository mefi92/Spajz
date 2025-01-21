using SpajzManager.Api.Controllers;
using SpajzManager.Api.Models;

namespace SpajzManager.Api
{
    public class HouseholdDataStore
    {
        public List<HouseholdDto> Households { get; set; }
        public static HouseholdDataStore Current { get; } = new HouseholdDataStore();

        public HouseholdDataStore()
        {
            Households = new List<HouseholdDto>()
            {
                new HouseholdDto()
                {
                    Id = 1,
                    Name = "Városlődi kecó",
                    Description = "Szülői ház",
                    Items = new List<HouseholdItemDto>()
                    {
                        new HouseholdItemDto() {
                            Id = 1,
                            Name = "Alma",
                            Description = "gyümölcs"},
                        new HouseholdItemDto() {
                            Id = 2,
                            Name = "Tej",
                            Description = "egyenesen a tehénből"},
                        new HouseholdItemDto() {
                            Id = 3,
                            Name = "Brokkoli",
                            Description = "utlimate zöldség"}
                     }
                },
                new HouseholdDto()
                {
                    Id = 2,
                    Name = "Palotai kégli",
                    Description = "Albérlet",
                    Items = new List<HouseholdItemDto>()
                    {
                        new HouseholdItemDto() {
                            Id = 1,
                            Name = "Zabtej",
                            Description = "Vegán zab ital"},
                        new HouseholdItemDto() {
                            Id = 2,
                            Name = "Humusz",
                            Description = "Natúr, homemade"}
                     }
                },
                new HouseholdDto()
                {
                    Id = 3,
                    Name = "Györöki kisház",
                    Description = "Nyaraló",
                    Items = new List<HouseholdItemDto>()
                    {
                        new HouseholdItemDto() {
                            Id = 1,
                            Name = "Kávé",
                            Description = "instant"},
                        new HouseholdItemDto() {
                            Id = 2,
                            Name = "Oliva olaj",
                            Description = "extra szűz"}
                     }
                }
            };
        }
    }
}
