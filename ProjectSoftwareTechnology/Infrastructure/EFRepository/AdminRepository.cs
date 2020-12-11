using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class AdminRepository : Repository<Admin> , IAdminRepository
    {
        public AdminRepository(WebAppDBContext context) : base(context) { }
    }
}
