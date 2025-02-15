using Microsoft.EntityFrameworkCore;
using BakeMart.Entities;

namespace BakeMart.Data;

public class ItemContext : DbContext
{
    public ItemContext(DbContextOptions<ItemContext> options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasKey(u => u.Id);
    }
}
