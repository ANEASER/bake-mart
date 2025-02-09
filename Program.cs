
using BakeMart.Data;
using BakeMart.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(options => options.UseSqlite("Data Source=BakeMart.db"));

var app = builder.Build();

app.MapUserEndpoints();

app.Run();
