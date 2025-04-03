using SpajzManager.Api.Entities;

namespace SpajzManager.Api.Services
{
    public interface ISpajzManagerRepository
    {
        Task<IEnumerable<Household>> GetHouseholdsAsync();
        Task<Household?> GetHouseholdAsync(int householdId, bool includeItems, bool includeStorages);
        Task<bool> HouseholdExistsAsync(int householdId);
        Task<bool> HouseholdExistsAsync(string householdName);
        Task AddHouseholdAsync(Household household);
        Task<IEnumerable<Item>> GetItemsForHouseholdAsync(int householdId);
        Task<(IEnumerable<Item>, PaginationMetadata)> GetItemsForHouseholdAsync(
            int householdId, string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Item?> GetItemForHouseholdAsync(int householdId, int itemId);
        Task AddItemForHouseholdAsync(int householdId, Item item);
        void DeleteItem(Item item);
        Task<bool> SaveChangesAsync();
        Task<bool> StorageExistsAsync(int householdId, int storageId);
        Task AddItemForStorageAsync(int storageId, Item item);
        Task<IEnumerable<Storage>> GetStoragesForHouseholdAsync(int householdId);
        Task<Storage?> GetStorageForHouseholdAsync(int householdId, int storageId);
        Task AddStorageForHouseholdAsync(int householdId, Storage storage);
        void DeleteStorate(Storage storage);
    }
}
