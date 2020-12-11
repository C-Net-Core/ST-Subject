using Domain.Entities;
using Domain.Repositories.IEFRepository;
using Infrastructure.EFContext;
using Infrastructure.Repository;

namespace Infrastructure.EFRepository
{
    public class FunctionRepository : Repository<Function> , IFunctionRepository
    {
        public FunctionRepository(WebAppDBContext context) : base(context) { }
    }
}
