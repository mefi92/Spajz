﻿using Microsoft.EntityFrameworkCore;
using SpajzManager.Api.DbContexts;
using SpajzManager.Api.Entities;

namespace SpajzManager.Api.Services
{
    public class SpajzManagerRepository : ISpajzManagerRepository
    {
        private readonly SpajzManagerContext _context;

        public SpajzManagerRepository(SpajzManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Household?> GetHouseholdAsync(
            int householdId,
            bool includeItems)
        {
            if (includeItems)
            {
                return await _context.Households.Include(h => h.Items)
                    .Where(h => h.Id == householdId).FirstOrDefaultAsync();
            }

            return await _context.Households.Include(s => s.Storages)
                .Where(h => h.Id == householdId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Household>> GetHouseholdsAsync()
        {
            return await _context.Households.OrderBy(h => h.Name).ToListAsync();
        }

        public async Task<bool> HouseholdExistsAsync(int householdId)
        {
            return await _context.Households.AnyAsync(h => h.Id == householdId);
        }

        public async Task<Item?> GetItemForHouseholdAsync(
            int householdId, 
            int itemId)
        {
            return await _context.Items
                .Where(i => i.HouseholdId == householdId && i.Id == itemId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsForHouseholdAsync(
            int householdId)
        {
            return await _context.Items
                .Where(i => i.HouseholdId == householdId)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }
        public async Task<(IEnumerable<Item>, PaginationMetadata)> GetItemsForHouseholdAsync(
            int householdId, string? name, string? searchQuery,
            int pageNumber, int pageSize)
        {
            var collection = _context.Items as IQueryable<Item>;

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(i => i.Name == name);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(i => i.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task AddItemForHouseholdAsync(int householdId, 
            Item item)
        {
            var household = await GetHouseholdAsync(householdId, false);
            if (household != null)
            {
                household.Items.Add(item);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void DeleteItem(Item item)
        {
            _context.Items.Remove(item);
        }

        public async Task<bool> StorageExistsAsync(int householdId, int storageId)
        {
            return await _context.Households
                .AnyAsync(h => h.Id == householdId && h.Storages.Any(s => s.Id == storageId));
        }

        public async Task AddItemToStorageAsync(int storageId, Item item)
        {
            var storage = await _context.Storages
                .FirstOrDefaultAsync(s => s.Id == storageId);

            if (storage == null)
            {
                throw new ArgumentException("Storage not found.");
            }

            item.StorageId = storageId;
            await _context.Items.AddAsync(item);
        }
    }
}
