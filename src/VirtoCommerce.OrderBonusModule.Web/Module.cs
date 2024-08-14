using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.OrderBonusModule.Core;
using VirtoCommerce.OrderBonusModule.Data.Repositories;
using System;
using VirtoCommerce.OrderBonusModule.Core.Services;
using VirtoCommerce.OrderBonusModule.Data.Handlers;
using VirtoCommerce.OrderBonusModule.Data.Services;
using VirtoCommerce.OrdersModule.Core.Events;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.StoreModule.Core.Model;

namespace VirtoCommerce.OrderBonusModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        // Initialize database
        var connectionString = Configuration.GetConnectionString(ModuleInfo.Id) ??
                               Configuration.GetConnectionString("VirtoCommerce");

        serviceCollection.AddDbContext<OrderBonusModuleDbContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.AddTransient<BonusRepository>();
        serviceCollection.AddTransient<Func<BonusRepository>>(provider =>
            () => provider.CreateScope().ServiceProvider.GetRequiredService<BonusRepository>());

        serviceCollection.AddTransient<IBonusService, BonusService>();

        serviceCollection.AddTransient<OrderChangedEventHandler>();
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        var serviceProvider = appBuilder.ApplicationServices;

        // Register settings
        var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>();
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);
        settingsRegistrar.RegisterSettingsForType(ModuleConstants.Settings.Bonus.AllSettings, nameof(Store));

        // Register permissions
        var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
        permissionsRegistrar.RegisterPermissions(ModuleInfo.Id, "OrderBonusModule", ModuleConstants.Security.Permissions.AllPermissions);

        // Apply migrations
        using var serviceScope = serviceProvider.CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<OrderBonusModuleDbContext>();
        dbContext.Database.Migrate();

        // Register events
        appBuilder.RegisterEventHandler<OrderChangedEvent, OrderChangedEventHandler>();
    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
