using BakeMart.Data;
using BakeMart.Dtos.UserDtos;
using BakeMart.Entities;
using BakeMart.Mapping;
using Microsoft.Extensions.Caching.Memory;
using BakeMart.Common;

namespace BakeMart.Endpoints;

public static class UserEndpoints
{
    const string GetUserById = "GetUserById";
    const string CreateUser = "CreateUser";
    const string UsersCacheKey = "users_list"; 
    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users").AddEndpointFilter<ExceptionFilter>();

        group.MapGet("/{id:int}", (int id, UserContext dbContext, IMemoryCache cache) =>
        {
            string cacheKey = $"user_{id}";

            if (!cache.TryGetValue(cacheKey, out User? user))
            {
                user = dbContext.Users.Find(id);
                if (user is null) return Results.NotFound();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                cache.Set(cacheKey, user, cacheOptions);
            }

            return Results.Ok(user);
        }).WithName(GetUserById);

        group.MapPost("/create", (CreateUserDto newUser, UserContext dbContext, IMemoryCache cache) =>
        {
            User user = newUser.ToEntity();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            string cacheKey = $"user_{user.Id}";
            cache.Set(cacheKey, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            cache.Remove(UsersCacheKey);

            return Results.Created($"/users/{user.Id}", user);
        });

        group.MapDelete("/{id:int}", (int id, UserContext dbContext, IMemoryCache cache) =>
        {
            var user = dbContext.Users.Find(id);
            if (user is null) return Results.NotFound();

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            cache.Remove($"user_{id}");
            cache.Remove(UsersCacheKey);

            return Results.NoContent();
        });

        return group;
    }
}
