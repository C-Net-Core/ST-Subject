using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class PayOrderCancelRepository : Repository<PayOrderCancel> , IPayOrderCancelRepository
    {
        public PayOrderCancelRepository(WebAppDBContext context) : base(context) { }
    }
}
