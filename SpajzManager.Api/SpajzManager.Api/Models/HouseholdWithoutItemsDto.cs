﻿namespace SpajzManager.Api.Models
{
    public class HouseholdWithoutItemsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
