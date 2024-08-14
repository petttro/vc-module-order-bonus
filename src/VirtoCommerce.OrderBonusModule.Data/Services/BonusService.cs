using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.OrderBonusModule.Core.Events;
using VirtoCommerce.OrderBonusModule.Core.Models;
using VirtoCommerce.OrderBonusModule.Core.Services;
using VirtoCommerce.OrderBonusModule.Data.Models;
using VirtoCommerce.OrderBonusModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.OrderBonusModule.Data.Services;
public class BonusService : CrudService<Bonus, BonusEntity, BonusChangingEvent, BonusChangedEvent>, IBonusService
{
    public BonusService(
        Func<BonusRepository> repositoryFactory,
        IPlatformMemoryCache platformMemoryCache,
        IEventPublisher eventPublisher) : base(repositoryFactory, platformMemoryCache, eventPublisher)
    {

    }

    protected override Task<IList<BonusEntity>> LoadEntities(IRepository repository, IList<string> ids, string responseGroup)
    {
        return ((BonusRepository)repository).GetBonusesByIdsAsync(ids);
    }
}
