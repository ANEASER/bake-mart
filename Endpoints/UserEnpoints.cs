using BakeMart.Data;
using BakeMart.Dtos.UserDtos;
using BakeMart.Entities;
using BakeMart.Mapping;

namespace BakeMart.Endpoints;

public static class UserEnpoints{

   const string GetUserbyId = "GetUserbyId";
   const string CreateUser = "CreateUser";

   public static RouteGroupBuilder MapUserEndpoints(this WebApplication app){

      var group = app.MapGroup("users");

      group.MapGet("/{id}", (int id, UserContext dbContext) =>{
         var user = dbContext.Users.Find(id);
         return user is not null ? Results.Ok(user) : Results.NotFound();
      }).WithName("GetUser");

      group.MapPost("/create", (CreateUserDto newUser, UserContext dbContext) => {
         User user = newUser.ToEntity();
         dbContext.Users.Add(user);
         dbContext.SaveChanges();
         return Results.Created($"/users/{user.Id}", user);
      });

      group.MapDelete("/{id}", (int id, UserContext dbContext) => {
         var user = dbContext.Users.Find(id);
         if(user is null) return Results.NotFound();
         dbContext.Users.Remove(user);
         dbContext.SaveChanges();
         return Results.NoContent();
      });

      return group;
   }

}