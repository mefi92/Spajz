﻿using SpajzManager.Api.Enums;

namespace SpajzManager.Api.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public QuantityUnit Unit { get; set; }
    }
}
