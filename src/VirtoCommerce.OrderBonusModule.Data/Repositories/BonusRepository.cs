using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.OrderBonusModule.Data.Models;
using VirtoCommerce.Platform.Core.Domain;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.OrderBonusModule.Data.Repositories;
public class BonusRepository : DbContextRepositoryBase<OrderBonusModuleDbContext>
{
    public BonusRepository(OrderBonusModuleDbContext dbContext, IUnitOfWork unitOfWork = null)
        : base(dbContext, unitOfWork)
    {
    }

    public IQueryable<BonusEntity> Bonuses => DbContext.Set<BonusEntity>();

    public virtual async Task<IList<BonusEntity>> GetBonusesByIdsAsync(IList<string> ids)
    {
        var result = await Bonuses.Where(x => ids.Contains(x.Id)).ToListAsync();

        return result;
    }

}
