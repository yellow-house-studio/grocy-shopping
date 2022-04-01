using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace YellowHouseStudio.GrocyShopping.DataAccess;

public class ProductMapDbContext : DbContext
{
    // public DbSet<Blog> Blogs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map table names
       /* modelBuilder.Entity<Blog>().ToTable("Blogs", "test");
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId);
            entity.HasIndex(e => e.Title).IsUnique();
            entity.Property(e => e.DateTimeAdd).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }); */
        base.OnModelCreating(modelBuilder);
    }
}