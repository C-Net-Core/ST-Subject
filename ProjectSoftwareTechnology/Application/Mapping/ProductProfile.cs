using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public static class ProductProfile
    {

        //hàm trả về 1 đối tượng của ProductDTO
        public static ProductDTO MappingProductDto(this Product product)
        {
            // gán value fields của Product cho value fields ProductDTO
            var productDTO = new ProductDTO
            {
                IDSP = product.IDSP,
                Name = product.Name,
                Cost = product.Cost,
                Descrip = product.Descrip,
                Image = product.Image,
                Amount = product.Amount,
                IDKM = product.IDKM,
                Size = product.Size,
                IDLSP = product.IDLSP
            };
            return productDTO;
        }



        //hàm trả về 1 đối tượng của Product
        public static Product MappingProduct(this ProductDTO productDto)
        {
            // gán value fields của Product cho value fields ProductDTO
            var product = new Product
            {
                IDSP = productDto.IDSP,
                Name = productDto.Name,
                Cost = productDto.Cost,
                Descrip = productDto.Descrip,
                Image = productDto.Image,
                Amount = productDto.Amount,
                IDKM = productDto.IDKM,
                Size = productDto.Size,
                IDLSP = productDto.IDLSP
            };
            return product;
        }



        // hàm gán giá trị của Product = ProductDTO
        public static void MappingProduct(this ProductDTO productDto, Product product)
        {
            // gán value fields của ProductDTO cho value fields Product
            product.IDSP = productDto.IDSP;
            product.Name = productDto.Name;
            product.Cost = productDto.Cost;
            product.Descrip = productDto.Descrip;
            product.Image = productDto.Image;
            product.Amount = productDto.Amount;
            product.IDKM = productDto.IDKM;
            product.Size = productDto.Size;
            product.IDLSP = productDto.IDLSP;
        }



        // hàm trả về danh sách ProductDTO và thực thi gán value của từng đối tượng Product cho ProductDTO
        public static IEnumerable<ProductDTO> MappingProductDtos(this IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                yield return product.MappingProductDto();
            }
        }
    }
}
