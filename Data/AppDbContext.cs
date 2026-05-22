using Microsoft.EntityFrameworkCore;
using SmartAssetTracking.Models;

namespace SmartAssetTracking.Data;

public class AppDbContext : DbContext
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Office> Offices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=assets.db");
    }
}