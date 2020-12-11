using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class SaleRepository : Repository<Sale> , ISaleRepository
    {
        public SaleRepository(WebAppDBContext context) : base(context) { }
    }
}
