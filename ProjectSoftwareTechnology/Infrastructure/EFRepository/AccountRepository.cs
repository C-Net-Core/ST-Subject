using Domain.Entities;
using Domain.Repositories.IEFRepository;
using Infrastructure.EFContext;
using Infrastructure.Repository;

namespace Infrastructure.EFRepository
{
    public class AccountRepository : Repository<Account> , IAccountRepository
    {
        public AccountRepository(WebAppDBContext context) : base(context) { }
    }
}
