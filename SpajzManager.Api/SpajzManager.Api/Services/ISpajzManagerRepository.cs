using SpajzManager.Api.Entities;

namespace SpajzManager.Api.Services
{
    public interface ISpajzManagerRepository
    {
        Task<IEnumerable<Household>> GetHouseholdsAsync();
        Task<Household?> GetHouseholdAsync(int householdId, bool includeItems);
        Task<bool> HouseholdExistsAsync(int householdId);
        Task<IEnumerable<Item>> GetItemsForHouseholdAsync(int householdId);
        Task<IEnumerable<Item>> GetItemsForHouseholdAsync(
            int householdId, string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Item?> GetItemForHouseholdAsync(int householdId, int itemId);
        Task AddItemForHouseholdAsync(int householdId, Item item);
        void DeleteItem(Item item);
        Task<bool> SaveChangesAsync();
    }
}
