using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public static class SaleProfile
    {

        public static SaleDTO MappingSaleDto(this Sale sale)
        {
            var saleDTO = new SaleDTO
            {
                IDKM = sale.IDKM,
                phantram = sale.phantram
            };
            return saleDTO;
        }


        public static Sale MappingSale(this SaleDTO saleDTO)
        {
            var sale = new Sale
            {
                IDKM = saleDTO.IDKM,
                phantram = saleDTO.phantram
            };
            return sale;
        }

        public static void MappingSale(this SaleDTO saleDTO, Sale sale)
        {
            sale.IDKM = saleDTO.IDKM;
            sale.phantram = saleDTO.phantram;
        }


        public static IEnumerable<SaleDTO> MappingSaleDtos(this IEnumerable<Sale> sales)
        {
            foreach (var sale in sales)
            {
                yield return sale.MappingSaleDto();
            }
        }
    }
}
