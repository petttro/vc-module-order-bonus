using System.Collections.Generic;
using VirtoCommerce.OrderBonusModule.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.OrderBonusModule.Core.Events;

public class BonusChangedEvent : GenericChangedEntryEvent<Bonus>
{
    public BonusChangedEvent(IEnumerable<GenericChangedEntry<Bonus>> changedEntries)
        : base(changedEntries)
    {
    }
}
