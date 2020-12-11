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
    public class ProductTypeService : IProductTypeDTOService
    {
        private readonly IProductTypeRepository productTypeRepository;
        public ProductTypeService(IProductTypeRepository productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }
        public void AddProductType(ProductTypeDTO productTypeDTO)
        {
            var productType = productTypeDTO.MappingProductType();
            productTypeRepository.Add(productType);
        }

        public void DeleteProductType(int id)
        {
            var productType = productTypeRepository.GetBy(id);
            productTypeRepository.Delete(productType);
        }

        public IEnumerable<ProductTypeDTO> GetAllProductType()
        {
            IEnumerable<ProductType> list = productTypeRepository.GetAll();
            return list.MappingProductTypeDtos();
        }

        public ProductTypeDTO GetProductTypeByID(int id)
        {
            var productType = productTypeRepository.GetBy(id);
            return productType.MappingProductTypeDto();
        }

        public void UpdateProductType(ProductTypeDTO productTypeDTO)
        {
            var productType = productTypeRepository.GetBy(productTypeDTO.IDLSP);

            productTypeDTO.MappingProductType(productType);

            productTypeRepository.Update(productType);
        }
    }
}
