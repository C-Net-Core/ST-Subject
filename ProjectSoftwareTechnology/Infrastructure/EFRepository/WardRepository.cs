using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class WardRepository : Repository<Ward> , IWardRepository
    {
        public WardRepository(WebAppDBContext context) : base(context) { }
    }
}
