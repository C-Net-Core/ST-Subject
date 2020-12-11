using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class ProductTypeRepository : Repository<ProductType> , IProductTypeRepository
    {
        public ProductTypeRepository(WebAppDBContext context) : base(context) { }
    }
}
