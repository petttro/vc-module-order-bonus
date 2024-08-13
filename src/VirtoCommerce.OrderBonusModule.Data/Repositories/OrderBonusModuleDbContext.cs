using Microsoft.EntityFrameworkCore;
using VirtoCommerce.OrderBonusModule.Data.Models;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.OrderBonusModule.Data.Repositories;

public class OrderBonusModuleDbContext : DbContextBase
{
    public OrderBonusModuleDbContext(DbContextOptions<OrderBonusModuleDbContext> options)
        : base(options)
    {
    }

    protected OrderBonusModuleDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BonusEntity>().ToTable("Bonuses").HasKey(x => x.Id);
        modelBuilder.Entity<BonusEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
        modelBuilder.Entity<BonusEntity>().Property(x => x.BonusTotal);
        modelBuilder.Entity<BonusEntity>().Property(x => x.BonusType).HasMaxLength(128);
        modelBuilder.Entity<BonusEntity>().Property(x => x.CustomerId).HasMaxLength(128);
        modelBuilder.Entity<BonusEntity>().Property(x => x.OrderId).HasMaxLength(128);
    }
}
