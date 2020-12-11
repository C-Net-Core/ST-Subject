using Domain.Entities;
using Domain.Repositories.IEFRepository;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EFRepository
{
    public class ProductHasDeletedRepository : Repository<ProductHasDeleted> , IProductHasDeletedRepository
    {
        public ProductHasDeletedRepository(WebAppDBContext context) : base(context) { }
    }
}
