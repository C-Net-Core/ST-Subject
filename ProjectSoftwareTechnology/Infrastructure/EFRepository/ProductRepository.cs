using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        public ProductRepository(WebAppDBContext context) : base(context) { }
    }
}
