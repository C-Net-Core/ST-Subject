using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.IEFRepository;
using System.Linq;

namespace Application.Services
{
    public class PayOrderService : IPayOrderDTOService
    {
        private readonly IPayOrderRepository payOrderRepository;
        public PayOrderService(IPayOrderRepository payOrderRepository)
        {
            this.payOrderRepository = payOrderRepository;
        }
        public void AddPayOrder(PayOrderDTO payOrder)
        {
            var pay = payOrder.MappingPayOrder();
            payOrderRepository.Add(pay);
        }

        public int countAllOrder()
        {
            return payOrderRepository.GetAll().Count();
        }

        public int countStatus(string status)
        {
            var orders = payOrderRepository.GetAll();
            var count = 0;
            foreach (var item in orders)
            {
                if (item.Status == status)
                {
                    count++;
                }
            }
            //return payOrderRepository.GetAll().Where(p => p.Status == status).Count();
            return count;
        }

        public void DeletePayOrder(int id)
        {
            var payOrder = payOrderRepository.GetBy(id);
            payOrderRepository.Delete(payOrder);
        }

        public IEnumerable<PayOrderDTO> GetAllPayOrder()
        {
            IEnumerable<PayOrder> payOrders = payOrderRepository.GetAll();
            return payOrders.MappingPayOrderDtos();
        }

        public PayOrderDTO GetPayOrderByID(int id)
        {
            var payOrder = payOrderRepository.GetBy(id);
            return payOrder.MappingPayOrdertDto();
        }

        public void UpdatePayOrder(PayOrderDTO payOrder)
        {
            var pay = payOrderRepository.GetBy(payOrder.ID);
            payOrder.MappingPayOrder(pay);
            payOrderRepository.Update(pay);
        }
    }
}
