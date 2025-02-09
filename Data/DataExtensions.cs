using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BakeMart.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
            
            // Ensure the database is created and migrated at startup
            dbContext.Database.Migrate();

            // Optionally, you can add a log or additional actions here
            Console.WriteLine("Database migrated successfully.");
        }
    }
}
