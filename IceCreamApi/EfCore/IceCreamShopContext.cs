using IceCreamApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace IceCreamApi;

public class IceCreamShopContext : DbContext
{
  public IceCreamShopContext(DbContextOptions options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<IceCream>().Property(i => i.Price).HasColumnType("decimal").HasPrecision(2,10);
    base.OnModelCreating(modelBuilder);
  }

  public DbSet<IceCream> IceCreams { get; set; } = null!;
  public DbSet<Account> Accounts { get; set; } = null!;
}