using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public static class ProductHasDeletedProfile
    {
        public static ProductHasDeletedDTO MappingProductHasDeletedDto(this ProductHasDeleted productHasDeleted)
        {
            var productHasDeletedDTO = new ProductHasDeletedDTO
            {
                ID = productHasDeleted.ID,
                IDSP = productHasDeleted.IDSP,
                IDLSP = productHasDeleted.IDLSP,
                Name = productHasDeleted.Name,
                NameBrand = productHasDeleted.NameBrand,
                Cost = productHasDeleted.Cost,
                Descrip = productHasDeleted.Descrip,
                Image = productHasDeleted.Image,
                Amount = productHasDeleted.Amount,
                IDKM = productHasDeleted.IDKM,
                Size = productHasDeleted.Size
            };
            return productHasDeletedDTO;
        }



        public static ProductHasDeleted MappingProductHasDeleted(this ProductHasDeletedDTO productHasDeletedDto)
        {
            var productHasDeleted = new ProductHasDeleted
            {
                ID = productHasDeletedDto.ID,
                IDSP = productHasDeletedDto.IDSP,
                IDLSP = productHasDeletedDto.IDLSP,
                Name = productHasDeletedDto.Name,
                NameBrand = productHasDeletedDto.NameBrand,
                Cost = productHasDeletedDto.Cost,
                Descrip = productHasDeletedDto.Descrip,
                Image = productHasDeletedDto.Image,
                Amount = productHasDeletedDto.Amount,
                IDKM = productHasDeletedDto.IDKM,
                Size = productHasDeletedDto.Size
            };
            return productHasDeleted;
        }


        public static void MappingProductHasDeleted(this ProductHasDeletedDTO productHasDeletedDTO
                                                        , ProductHasDeleted productHasDeleted)
        {
            productHasDeleted.ID = productHasDeletedDTO.ID;
            productHasDeleted.IDSP = productHasDeletedDTO.IDSP;
            productHasDeleted.IDLSP = productHasDeletedDTO.IDLSP;
            productHasDeleted.Name = productHasDeletedDTO.Name;
            productHasDeleted.NameBrand = productHasDeletedDTO.NameBrand;
            productHasDeleted.Cost = productHasDeletedDTO.Cost;
            productHasDeleted.Descrip = productHasDeletedDTO.Descrip;
            productHasDeleted.Image = productHasDeletedDTO.Image;
            productHasDeleted.Amount = productHasDeletedDTO.Amount;
            productHasDeleted.IDKM = productHasDeletedDTO.IDKM;
            productHasDeleted.Size = productHasDeletedDTO.Size;
        }


        public static IEnumerable<ProductHasDeletedDTO> MappingAccountDtos(this IEnumerable<ProductHasDeleted> productHasDeleteds)
        {
            foreach (var item in productHasDeleteds)
            {
                yield return item.MappingProductHasDeletedDto();
            }
        }
    }
}
