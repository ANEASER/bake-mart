using BakeMart.Entities;
using System;
using BakeMart.Dtos.ItemDtos;

namespace BakeMart.Mapping
{
    public static class ItemMapping
    {
        public static Item ToEntity(this CreateItemDto itemDto)
        {
            return new Item()
            {
                ItemName = itemDto.ItemName,
                Description = itemDto.Description,
                Price = itemDto.Price,
                ImageLink = itemDto.ImageLink
            };
        }
        
        public static ItemDto FromDto(this Item item)
        {
            return new ItemDto(
                item.Id,
                item.ItemName,
                item.Description,
                item.Price,
                item.ImageLink
            );
        }
    }
}