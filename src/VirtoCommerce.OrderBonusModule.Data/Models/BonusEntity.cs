using System;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.OrderBonusModule.Core.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;

namespace VirtoCommerce.OrderBonusModule.Data.Models;
public class BonusEntity : AuditableEntity, IDataEntity<BonusEntity, Bonus>
{
    public string CustomerId { get; set; }

    public string OrderId { get; set; }

    [Precision(18, 6)]
    public decimal BonusTotal { get; set; }

    public string BonusType { get; set; }


    public BonusEntity FromModel(Bonus model, PrimaryKeyResolvingMap pkMap)
    {
        pkMap.AddPair(model, this);

        Id = model.Id;
        CustomerId = model.CustomerId;
        OrderId = model.OrderId;
        BonusTotal = model.BonusTotal;
        BonusType = model.BonusType;

        return this;
    }

    public void Patch(BonusEntity target)
    {
        target.Id = Id;
    }

    public Bonus ToModel(Bonus model)
    {
        model.Id = Id;
        model.BonusTotal = BonusTotal;
        model.BonusType = BonusType;
        model.CustomerId = CustomerId;
        model.OrderId = OrderId;

        return model;
    }
}
