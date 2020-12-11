using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories.IEFRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductService : IProductDTOService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<ProductDTO> GetAllProduct()
        {
            IEnumerable<Product> products = productRepository.GetAll();
            return products.MappingProductDtos();
        }

        public ProductDTO GetProductByID(int id)
        {
            var product = productRepository.GetBy(id);
            return product.MappingProductDto();
        }

        public void AddProduct(ProductDTO productDTO)
        {
            var product = productDTO.MappingProduct();
            productRepository.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var product = productRepository.GetBy(id);
            productRepository.Delete(product);
        }

        public void UpdateProduct(ProductDTO productDTO)
        {
            var product = productRepository.GetBy(productDTO.IDSP);
            productDTO.MappingProduct(product);
            productRepository.Update(product);
        }
    }
}
