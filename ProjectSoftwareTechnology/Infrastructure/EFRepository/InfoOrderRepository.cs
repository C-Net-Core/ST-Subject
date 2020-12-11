﻿using Domain.Entities;
using Infrastructure.EFContext;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;

namespace Infrastructure.EFRepository
{
    public class InfoOrderRepository : Repository<InfoOrder> , IInfoOrderRepository
    {
        public InfoOrderRepository(WebAppDBContext context) : base(context) { }
    }
}
