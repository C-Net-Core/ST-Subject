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
    public class PayOrderCancelService : IPayOrderCancelDTOService
    {
        private readonly IPayOrderCancelRepository payOrderCancelRepository;
        public PayOrderCancelService(IPayOrderCancelRepository payOrderCancelRepository)
        {
            this.payOrderCancelRepository = payOrderCancelRepository;
        }
        public void AddPayOrderCancel(PayOrderCancelDTO payOrderCancel)
        {
            var pay = payOrderCancel.MappingPayOrderCancel();
            payOrderCancelRepository.Add(pay);
        }

        public void DeletePayOrderCancel(int id)
        {
            var payOrderCancel = payOrderCancelRepository.GetBy(id);
            payOrderCancelRepository.Delete(payOrderCancel);
        }

        public IEnumerable<PayOrderCancelDTO> GetAllPayOrderCancel()
        {
            IEnumerable<PayOrderCancel> payOrderCancels = payOrderCancelRepository.GetAll();
            return payOrderCancels.MappingPayOrderCancelDtos();
        }

        public PayOrderCancelDTO GetPayOrderCancelByID(int id)
        {
            var payOrderCancel = payOrderCancelRepository.GetBy(id);
            return payOrderCancel.MappingPayOrdertCancelDto();
        }

        public void UpdatePayOrderCancel(PayOrderCancelDTO payOrderCancel)
        {
            var pay = payOrderCancelRepository.GetBy(payOrderCancel.ID);
            payOrderCancel.MappingPayOrderCancel(pay);
            payOrderCancelRepository.Update(pay);
        }

        public int countAllOrder()
        {
            throw new NotImplementedException();
        }

        public int countStatus(string status)
        {
            throw new NotImplementedException();
        }

    }
}
