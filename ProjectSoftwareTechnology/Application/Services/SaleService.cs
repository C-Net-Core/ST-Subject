using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.IEFRepository;

namespace Application.Services
{
    public class SaleService : ISaleDTOService
    {
        private readonly ISaleRepository saleRepository;
        public SaleService(ISaleRepository saleRepository)
        {
            this.saleRepository = saleRepository;
        }
        public void AddSale(SaleDTO sale)
        {
            var saleEF = sale.MappingSale();
            saleRepository.Add(saleEF);
        }

        public void DeleteSale(int id)
        {
            var saleEF = saleRepository.GetBy(id);
            saleRepository.Delete(saleEF);
        }

        public IEnumerable<SaleDTO> GetAllSale()
        {
            IEnumerable<Sale> list = saleRepository.GetAll();
            return list.MappingSaleDtos();
        }

        public SaleDTO GetSaleByID(int id)
        {
            var saleEF = saleRepository.GetBy(id);
            return saleEF.MappingSaleDto();
        }

        public void UpdateSale(SaleDTO sale)
        {
            var saleEF = saleRepository.GetBy(sale.IDKM);
            sale.MappingSale(saleEF);
            saleRepository.Update(saleEF);
        }
    }
}
