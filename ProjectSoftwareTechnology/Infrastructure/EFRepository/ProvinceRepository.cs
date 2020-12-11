using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class ProvinceRepository : Repository<Province> , IProvinceRepository
    {
        public ProvinceRepository(WebAppDBContext context) : base(context) { }
    }
}
