using Domain.Entities;
using Domain.Repositories.IEFRepository;
using Infrastructure.EFContext;
using Infrastructure.Repository;

namespace Infrastructure.EFRepository
{
    public class AuthorizationRepository : Repository<Authorization> , IAuthorizationRepository
    {
        public AuthorizationRepository(WebAppDBContext context) : base(context) { }
    }
}
