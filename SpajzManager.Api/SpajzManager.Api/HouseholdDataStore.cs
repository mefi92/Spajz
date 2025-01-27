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
                    Items = new List<ItemDto>()
                    {
                        new ItemDto() {
                            Id = 1,
                            Name = "Alma",
                            Description = "gyümölcs"},
                        new ItemDto() {
                            Id = 2,
                            Name = "Tej",
                            Description = "egyenesen a tehénből"},
                        new ItemDto() {
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
                    Items = new List<ItemDto>()
                    {
                        new ItemDto() {
                            Id = 1,
                            Name = "Zabtej",
                            Description = "Vegán zab ital"},
                        new ItemDto() {
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
                    Items = new List<ItemDto>()
                    {
                        new ItemDto() {
                            Id = 1,
                            Name = "Kávé",
                            Description = "instant"},
                        new ItemDto() {
                            Id = 2,
                            Name = "Oliva olaj",
                            Description = "extra szűz"}
                     }
                }
            };
        }
    }
}
