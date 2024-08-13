using System.Collections.Generic;
using VirtoCommerce.OrderBonusModule.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.OrderBonusModule.Core.Events;

public class BonusChangingEvent : GenericChangedEntryEvent<Bonus>
{
    public BonusChangingEvent(IEnumerable<GenericChangedEntry<Bonus>> changedEntries)
        : base(changedEntries)
    {
    }
}
