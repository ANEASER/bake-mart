using BakeMart.Data;
using BakeMart.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure authentication (JWT token example)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere")),
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience"
        };
    });

// Add DbContexts
builder.Services.AddDbContext<UserContext>(options => options.UseSqlite("Data Source=BakeMart.db"));
builder.Services.AddDbContext<ItemContext>(options => options.UseSqlite("Data Source=BakeMart.db"));

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapItemEndpoints();

app.Run();
