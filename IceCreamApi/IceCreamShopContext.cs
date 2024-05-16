using IceCreamApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace IceCreamApi;

public class IceCreamShopContext : DbContext
{
  public IceCreamShopContext(DbContextOptions options) : base(options)
  {
  }
  public DbSet<IceCream> IceCreams { get; set; } = null!;
}