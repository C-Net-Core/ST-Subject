using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public static class ProductTypeProfile
    {

        public static ProductTypeDTO MappingProductTypeDto(this ProductType productType)
        {
            var productTypeDTO = new ProductTypeDTO
            {
                IDLSP = productType.IDLSP,
                Name = productType.Name,
                Filter = productType.Filter,
            };
            return productTypeDTO;
        }



        public static ProductType MappingProductType(this ProductTypeDTO productTypeDTO)
        {
            var productType = new ProductType
            {
                IDLSP = productTypeDTO.IDLSP,
                Name = productTypeDTO.Name,
                Filter = productTypeDTO.Filter,
            };
            return productType;
        }



        public static void MappingProductType(this ProductTypeDTO productTypeDTO, ProductType productType)
        {
            productType.IDLSP = productTypeDTO.IDLSP;
            productType.Name = productTypeDTO.Name;
            productType.Filter = productTypeDTO.Filter;
        }



        public static IEnumerable<ProductTypeDTO> MappingProductTypeDtos(this IEnumerable<ProductType> productTypes)
        {
            foreach (var productType in productTypes)
            {
                yield return productType.MappingProductTypeDto();
            }
        }
    }
}
