using BakeMart.Data;
using BakeMart.Dtos.UserDtos;
using BakeMart.Entities;
using BakeMart.Mapping;
using BakeMart.Common;
using Microsoft.AspNetCore.Authorization;

namespace BakeMart.Endpoints;

public static class UserEndpoints
{
    const string GetUserById = "GetUserById";
    const string CreateUser = "CreateUser";
    const string UpdateUser = "UpdateUser";
    const string DeleteUser = "DeleteUser";

    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users").AddEndpointFilter<ExceptionFilter>();

        group.MapGet("/{id:int}", [Authorize(Roles = "Admin, User")] (int id, UserContext dbContext) =>
            {
                var user = dbContext.Users.Find(id);
                if (user is null) return Results.NotFound();

                return Results.Ok(user);
            }).WithName(GetUserById);

        group.MapPost("/create", [Authorize(Roles = "Admin")] (CreateUserDto newUser, UserContext dbContext) =>
            {
                User user = newUser.ToEntity();
                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                return Results.Created($"/users/{user.Id}", user);
            }).WithName(CreateUser);


        group.MapDelete("/{id:int}", [Authorize(Roles = "Admin")] (int id, UserContext dbContext) =>
            {
                var user = dbContext.Users.Find(id);
                if (user is null) return Results.NotFound();

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();

                return Results.NoContent();
            }).WithName(DeleteUser);

        return group;
    }
}
