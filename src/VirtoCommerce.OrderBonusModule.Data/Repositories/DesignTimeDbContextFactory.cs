using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VirtoCommerce.OrderBonusModule.Data.Repositories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderBonusModuleDbContext>
{
    public OrderBonusModuleDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<OrderBonusModuleDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=localhost\\SQLEXPRESS;User=virto;Password=virto;Database=VirtoCommerce3.net8;TrustServerCertificate=True;";

        builder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

        return new OrderBonusModuleDbContext(builder.Options);
    }
}
