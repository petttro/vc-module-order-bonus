using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.OrderBonusModule.Core;
using VirtoCommerce.OrderBonusModule.Core.Models;
using VirtoCommerce.OrderBonusModule.Core.Services;
using VirtoCommerce.OrdersModule.Core.Events;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.OrderBonusModule.Data.Handlers;
public class OrderChangedEventHandler : IEventHandler<OrderChangedEvent>
{
    private const string Vip = "vip";
    private const string SuperVip = "supervip";

    readonly IBonusService _bonusService;
    private readonly ISettingsManager _settingsManager;

    private int _normalCustomerPercentSetting;
    private int _vipCustomerPercentSetting;
    private int _superVipCustomerPercentSetting;

    public OrderChangedEventHandler(IBonusService bonusService, ISettingsManager settingsManager)
    {
        _bonusService = bonusService;
        _settingsManager = settingsManager;
    }

    public async Task Handle(OrderChangedEvent message)
    {
        _normalCustomerPercentSetting = await _settingsManager.GetValueAsync<int>(ModuleConstants.Settings.Bonus.BonusPercentNormal);
        _vipCustomerPercentSetting = await _settingsManager.GetValueAsync<int>(ModuleConstants.Settings.Bonus.BonusPercentVip);
        _superVipCustomerPercentSetting = await _settingsManager.GetValueAsync<int>(ModuleConstants.Settings.Bonus.BonusPercentSuperVip);

        var bonuses = new List<Bonus>();
        foreach (var changedOrder in message.ChangedEntries)
        {
            var oldStatus = changedOrder.OldEntry.Status;
            var newStatus = changedOrder.NewEntry.Status;

            if (oldStatus != newStatus && newStatus == ModuleConstants.CustomerOrder.Status.Completed)
            {
                var bonus = CalculateBonus(changedOrder.NewEntry);
                bonuses.Add(bonus);
            }
        }

        await _bonusService.SaveChangesAsync(bonuses);
    }

    private Bonus CalculateBonus(CustomerOrder customerOrder)
    {
        // TODO: To determine which field to use to get CustomerType (for now hard-coded to VIP)
        var bonusType = "VIP";

        var percent = bonusType.ToLower() switch
        {
            ModuleConstants.Customer.Type.Vip => _vipCustomerPercentSetting,
            ModuleConstants.Customer.Type.SuperVip => _superVipCustomerPercentSetting,
            _ => _normalCustomerPercentSetting,
        };

        var bonus = new Bonus()
        {
            CustomerId = customerOrder.CustomerId,
            OrderId = customerOrder.Id,
            BonusType = bonusType,
            BonusTotal = customerOrder.SubTotal / 100 * percent
        };

        return bonus;
    }
}
