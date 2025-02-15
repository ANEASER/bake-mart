using BakeMart.Data;
using BakeMart.Dtos.ItemDtos;
using BakeMart.Entities;
using BakeMart.Mapping;
using Microsoft.Extensions.Caching.Memory;
using BakeMart.Common;

namespace BakeMart.Endpoints;


public static class ItemEndpoints
{
    const string GetItemById = "GetItemById";
    const string CreateItem = "CreateItem";
    const string ItemsCacheKey = "items_list";

    public static RouteGroupBuilder MapItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("items").AddEndpointFilter<ExceptionFilter>();

        group.MapGet("/{id:int}", (int id, ItemContext dbContext, IMemoryCache cache) =>
        {
            string cacheKey = $"item_{id}";

            if (!cache.TryGetValue(cacheKey, out Item? item))
            {
                item = dbContext.Items.Find(id);
                if (item is null) return Results.NotFound();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                cache.Set(cacheKey, item, cacheOptions);
            }

            return Results.Ok(item);
        }).WithName(GetItemById);

        group.MapPost("/create", (CreateItemDto newItem, ItemContext dbContext, IMemoryCache cache) =>
        {
            Item item = newItem.ToEntity();
            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            string cacheKey = $"item_{item.Id}";
            cache.Set(cacheKey, item, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            cache.Remove(ItemsCacheKey);

            return Results.Created($"/items/{item.Id}", item);
        }).WithName(CreateItem);

        group.MapDelete("/{id:int}", (int id, ItemContext dbContext, IMemoryCache cache) =>
        {
            var item = dbContext.Items.Find(id);
            if (item is null) return Results.NotFound();

            dbContext.Items.Remove(item);
            dbContext.SaveChanges();

            cache.Remove($"item_{id}");
            cache.Remove(ItemsCacheKey);

            return Results.NoContent();
        }).WithName("DeleteItem");

        group.MapPut("/{id:int}", (int id, UpdateItemDto updatedItem, ItemContext dbContext, IMemoryCache cache) =>
        {
            var item = dbContext.Items.Find(id);
            if (item is null) return Results.NotFound();

            item.ItemName = updatedItem.ItemName;
            item.Description = updatedItem.Description;
            item.Price = updatedItem.Price;
            item.ImageLink = updatedItem.ImageLink;

            dbContext.SaveChanges();

            cache.Remove($"item_{id}");
            cache.Remove(ItemsCacheKey);

            return Results.Ok(item);
        }).WithName("UpdateItem");
        
        return group;
    }
}