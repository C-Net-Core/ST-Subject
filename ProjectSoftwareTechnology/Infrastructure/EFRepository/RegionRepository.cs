using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class RegionRepository : Repository<Region> , IRegionRepository
    {
        public RegionRepository(WebAppDBContext context) : base(context) { }
    }
}
