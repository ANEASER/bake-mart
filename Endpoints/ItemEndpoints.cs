using BakeMart.Data;
using BakeMart.Dtos.ItemDtos;
using BakeMart.Entities;
using BakeMart.Mapping;
using BakeMart.Common;
using Microsoft.AspNetCore.Authorization;

namespace BakeMart.Endpoints;

public static class ItemEndpoints
{
    const string GetItemById = "GetItemById";
    const string CreateItem = "CreateItem";

    public static RouteGroupBuilder MapItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("items").AddEndpointFilter<ExceptionFilter>();

        group.MapGet("/{id:int}",[Authorize(Roles = "Admin, User")] (int id, ItemContext dbContext) =>
        {
            var item = dbContext.Items.Find(id);
            if (item is null) return Results.NotFound();

            return Results.Ok(item);
        }).WithName(GetItemById);

        group.MapPost("/create",[Authorize(Roles = "Admin")] (CreateItemDto newItem, ItemContext dbContext) =>
        {
            Item item = newItem.ToEntity();
            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return Results.Created($"/items/{item.Id}", item);
        }).WithName(CreateItem);

        group.MapDelete("/{id:int}", [Authorize(Roles = "Admin")](int id, ItemContext dbContext) =>
        {
            var item = dbContext.Items.Find(id);
            if (item is null) return Results.NotFound();

            dbContext.Items.Remove(item);
            dbContext.SaveChanges();

            return Results.NoContent();
        }).WithName("DeleteItem");

        group.MapPut("/{id:int}",[Authorize(Roles = "Admin")] (int id, UpdateItemDto updatedItem, ItemContext dbContext) =>
        {
            var item = dbContext.Items.Find(id);
            if (item is null) return Results.NotFound();

            item.ItemName = updatedItem.ItemName;
            item.Description = updatedItem.Description;
            item.Price = updatedItem.Price;
            item.ImageLink = updatedItem.ImageLink;

            dbContext.SaveChanges();

            return Results.Ok(item);
        }).WithName("UpdateItem");

        return group;
    }
}
