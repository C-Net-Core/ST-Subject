using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Repositories.IEFRepository;

namespace Application.Services
{
    public class InfoOrderService : IInfoOrderDTOService
    {
        private readonly IInfoOrderRepository infoOrderRepository;
        public InfoOrderService(IInfoOrderRepository infoOrderRepository)
        {
            this.infoOrderRepository = infoOrderRepository;
        }
        public void AddInfoOrder(InfoOrderDTO infoOrder)
        {
            var info = infoOrder.MappingInfoOrder();
            infoOrderRepository.Add(info);
        }

        public void DeleteInfoOrder(int id)
        {
            var infoOrder = infoOrderRepository.GetBy(id);
            infoOrderRepository.Delete(infoOrder);
        }

        public IEnumerable<InfoOrderDTO> GetAllInfoOrder()
        {
            IEnumerable<InfoOrder> infoOrders = infoOrderRepository.GetAll();
            return infoOrders.MappingInfoOrderDtos();
        }

        public InfoOrderDTO GetInfoOrderByID(int id)
        {
            var infoOrder = infoOrderRepository.GetBy(id);
            return infoOrder.MappingInfoOrderDto();
        }

        public void UpdateInfoOrder(InfoOrderDTO infoOrder)
        {
            var info = infoOrderRepository
                .GetAll()
                .Where(i => i.IDP == infoOrder.IDP && i.IDSP == infoOrder.IDSP)
                .FirstOrDefault();
            infoOrder.MappingInfoOrder(info);
            infoOrderRepository.Update(info);
        }
    }
}
