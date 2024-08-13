using System.Linq;
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

}
