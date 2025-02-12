
using BakeMart.Data;
using BakeMart.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(options => options.UseSqlite("Data Source=BakeMart.db"));
builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapUserEndpoints();

app.Run();
