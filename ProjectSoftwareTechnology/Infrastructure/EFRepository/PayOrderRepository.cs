using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class PayOrderRepository : Repository<PayOrder> , IPayOrderRepository
    {
        public PayOrderRepository(WebAppDBContext context) : base(context) { }
    }
}
