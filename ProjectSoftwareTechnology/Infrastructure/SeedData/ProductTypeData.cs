
using Domain.Entities;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class ProductTypeData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.ProductTypes.Any()) return;
            context.ProductTypes.AddRange(new List<ProductType> {
                new ProductType
                {
                    Name = "Nike",
                    Filter = "nike"
                },
                new ProductType
                {
                    Name = "ASICS",
                    Filter = "asics"
                },
                new ProductType
                {
                    Name = "Adidas",
                    Filter = "adidas"
                },
                new ProductType
                {
                    Name = "New Balance",
                    Filter = "newbalance"
                },
                new ProductType
                {
                    Name = "Skechers",
                    Filter = "skechers"
                },
                new ProductType
                {
                    Name = "Fila",
                    Filter = "fila"
                },
                new ProductType
                {
                    Name = "Bata",
                    Filter = "bata"
                },
                new ProductType
                {
                    Name = "Burberry",
                    Filter = "burberry"
                },
                new ProductType
                {
                    Name = "VF Corporation",
                    Filter = "vfcorporation"
                },
                new ProductType
                {
                    Name = "PUMA",
                    Filter = "puma"
                }
            });

            context.SaveChanges();
        }
    }
}
