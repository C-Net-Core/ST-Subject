using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Repositories.IEFRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductHasDeletedService : IProductHasDeletedDTOService
    {
        private readonly IProductHasDeletedRepository productHasDeletedRepository;
        public ProductHasDeletedService(IProductHasDeletedRepository productHasDeletedRepository)
        {
            this.productHasDeletedRepository = productHasDeletedRepository;
        }

        public void Add(ProductHasDeletedDTO productDTO)
        {
            var product = productDTO.MappingProductHasDeleted();
            productHasDeletedRepository.Add(product);
        }

        public void Delete(int id)
        {
            var product = productHasDeletedRepository.GetBy(id);
            productHasDeletedRepository.Delete(product);
        }

        public IEnumerable<ProductHasDeletedDTO> GetAll()
        {
            var products = productHasDeletedRepository.GetAll();
            return products.MappingAccountDtos();
        }

        public ProductHasDeletedDTO GetByID(int id)
        {
            var product = productHasDeletedRepository.GetBy(id);
            return product.MappingProductHasDeletedDto();
        }
    }
}
