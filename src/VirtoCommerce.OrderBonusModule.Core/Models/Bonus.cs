using System;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.OrderBonusModule.Core.Models;
public class Bonus : AuditableEntity, ICloneable
{
    public string CustomerId { get; set; }

    public string OrderId { get; set; }

    public decimal BonusTotal { get; set; }

    public string BonusType { get; set; }

    public object Clone() => MemberwiseClone();
}
