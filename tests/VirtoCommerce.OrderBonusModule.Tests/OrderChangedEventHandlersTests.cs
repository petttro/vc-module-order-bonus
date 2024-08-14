using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using VirtoCommerce.OrderBonusModule.Core;
using VirtoCommerce.OrderBonusModule.Core.Models;
using VirtoCommerce.OrderBonusModule.Core.Services;
using VirtoCommerce.OrderBonusModule.Data.Handlers;
using VirtoCommerce.OrdersModule.Core.Events;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Core.Settings;
using Xunit;

namespace VirtoCommerce.OrderBonusModule.Tests;
public class OrderChangedEventHandlersTests : IDisposable
{
    private MockRepository _mockRepository;
    private Mock<IBonusService> _bonusServiceMock;
    private Mock<ISettingsManager> _settingsManagerMock;
    private OrderChangedEventHandler _eventHandler;

    public OrderChangedEventHandlersTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _bonusServiceMock = _mockRepository.Create<IBonusService>();
        _settingsManagerMock = _mockRepository.Create<ISettingsManager>();

        _eventHandler = new OrderChangedEventHandler(_bonusServiceMock.Object, _settingsManagerMock.Object);
    }

    [Fact]
    public async Task HandleEvent_Success()
    {
        var newEntry = new CustomerOrder
        {
            SubTotal = 1000,
            Status = ModuleConstants.CustomerOrder.Status.Completed
        };

        var oldEntry = new CustomerOrder
        {
            SubTotal = 1000,
            Status = ModuleConstants.CustomerOrder.Status.New
        };

        var changedEntries = new List<GenericChangedEntry<CustomerOrder>>
        {
            new GenericChangedEntry<CustomerOrder>(newEntry, oldEntry, EntryState.Modified)
        };
        var orderChangedEvent = new OrderChangedEvent(changedEntries);

        SetupLoadSetting();

        _bonusServiceMock
            .Setup(b => b.SaveChangesAsync(It.Is<List<Bonus>>(l => l[0].BonusTotal == 50)))
            .Returns(Task.CompletedTask);

        await _eventHandler.Handle(orderChangedEvent);
    }

    [Fact]
    public async Task HandleEvent_NotCompletedStatus_NotAddBonus()
    {
        var newEntry = new CustomerOrder
        {
            SubTotal = 1000,
            Status = ModuleConstants.CustomerOrder.Status.New
        };

        var oldEntry = new CustomerOrder
        {
            SubTotal = 1000,
            Status = ModuleConstants.CustomerOrder.Status.New
        };

        var changedEntries = new List<GenericChangedEntry<CustomerOrder>>
        {
            new GenericChangedEntry<CustomerOrder>(newEntry, oldEntry, EntryState.Modified)
        };
        var orderChangedEvent = new OrderChangedEvent(changedEntries);

        SetupLoadSetting();

        _bonusServiceMock
            .Setup(b => b.SaveChangesAsync(It.Is<List<Bonus>>(l => !l.Any())))
            .Returns(Task.CompletedTask);

        await _eventHandler.Handle(orderChangedEvent);
    }

    [Fact]
    public async Task HandleEvent_CompletedStatusWasNotChanged_NotAddBonus()
    {
        var newEntry = new CustomerOrder
        {
            SubTotal = 1000,
            Status = ModuleConstants.CustomerOrder.Status.Completed
        };

        var oldEntry = new CustomerOrder
        {
            SubTotal = 1000,
            Status = ModuleConstants.CustomerOrder.Status.Completed
        };

        var changedEntries = new List<GenericChangedEntry<CustomerOrder>>
        {
            new GenericChangedEntry<CustomerOrder>(newEntry, oldEntry, EntryState.Modified)
        };
        var orderChangedEvent = new OrderChangedEvent(changedEntries);

        SetupLoadSetting();       

        _bonusServiceMock
            .Setup(b => b.SaveChangesAsync(It.Is<List<Bonus>>(l => !l.Any())))
            .Returns(Task.CompletedTask);

        await _eventHandler.Handle(orderChangedEvent);
    }

    private void SetupLoadSetting()
    {
        _settingsManagerMock
           .Setup(s => s.GetObjectSettingAsync(ModuleConstants.Settings.Bonus.BonusPercentNormal.Name, null, null))
           .ReturnsAsync(new ObjectSettingEntry());

        _settingsManagerMock
           .Setup(s => s.GetObjectSettingAsync(ModuleConstants.Settings.Bonus.BonusPercentVip.Name, null, null))
           .ReturnsAsync(new ObjectSettingEntry());

        _settingsManagerMock
           .Setup(s => s.GetObjectSettingAsync(ModuleConstants.Settings.Bonus.BonusPercentSuperVip.Name, null, null))
           .ReturnsAsync(new ObjectSettingEntry());
    }

    public void Dispose()
    {
        _mockRepository.VerifyAll();
    }
}
