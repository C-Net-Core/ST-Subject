
using Domain.Entities;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class ProductData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any()) return;
            context.Products.AddRange(new List<Product> {
                new Product
                {
                  Name = "Nike Air Max 90",
                  Cost = "4390000",
                  Descrip = "This is so beautiful",    
                  Image = "product-1.jpg",
                  Amount = "3",
                  IDKM = 3,
                  Size = "40",
                  IDLSP = 1
                },
                new Product
                {
                  Name = "Nike Air Force 1",
                  Cost = "5129000",
                  Descrip = "This is so beautiful",
                  Image = "product-2.jpg",
                  Amount = "2",
                  IDKM = 2,
                  Size = "41",
                  IDLSP = 2
                },
                new Product
                {
                  Name = "Nike SF AF1",
                  Cost = "3320000",
                  Descrip = "This is so beautiful",
                  Image = "product-3.jpg",
                  Amount = "1",
                  IDKM = 1,
                  Size = "42",
                  IDLSP = 3
                },
                new Product
                {
                  Name = "Adidas Uptempo",
                  Cost = "5212000",
                  Descrip = "This is so beautiful",
                  Image = "product-4.jpg",
                  Amount = "3",
                  IDKM = 4,
                  Size = "39",
                  IDLSP = 4
                },
                new Product
                {
                  Name = "Nike Jordan 1",
                  Cost = "4390000",
                  Descrip = "This is so beautiful",
                  Image = "product-5.jpg",
                  Amount = "5",
                  IDKM = 1,
                  Size = "42",
                  IDLSP = 5
                },
                new Product
                {
                  Name = "Nike Uptempo Subasa",
                  Cost = "7290000",
                  Descrip = "This is so beautiful",
                  Image = "product-6.jpg",
                  Amount = "1",
                  IDKM = 3,
                  Size = "41",
                  IDLSP = 6
                },
                new Product
                {
                  Name = "Adidas VaporMax ACE",
                  Cost = "2618000",
                  Descrip = "This is so beautiful",
                  Image = "product-7.jpg",
                  Amount = "2",
                  IDKM = 3,
                  Size = "39",
                  IDLSP = 7
                },
                new Product
                {
                  Name = "Nike Cortez",
                  Cost = "2290000",
                  Descrip = "This is so beautiful",
                  Image = "product-8.jpg",
                  Amount = "40",
                  IDKM = 1,
                  Size = "41",
                  IDLSP = 8
                },
                new Product
                {
                  Name = "Asics Gel Saga",
                  Cost = "4390000",
                  Descrip = "This is so beautiful",
                  Image = "product-9.jpg",
                  Amount = "3",
                  IDKM = 2,
                  Size = "40",
                  IDLSP = 9
                },
                new Product
                {
                  Name = "Asics Gel Rocket",
                  Cost = "2390000",
                  Descrip = "This is so beautiful",
                  Image = "product-10.jpg",
                  Amount = "3",
                  IDKM = 2,
                  Size = "38",
                  IDLSP = 10
                },
                new Product
                {
                  Name = "Fw Women Tartheredge",
                  Cost = "3390000",
                  Descrip = "This is so beautiful",
                  Image = "product-1.jpg",
                  Amount = "3",
                  IDKM = 2,
                  Size = "40",
                  IDLSP = 5
                },
                new Product
                {
                  Name = "Fw Men Tartheredge",
                  Cost = "1390000",
                  Descrip = "This is so beautiful",
                  Image = "product-10.jpg",
                  Amount = "2",
                  IDKM = 2,
                  Size = "38",
                  IDLSP = 4
                },
                new Product
                {
                  Name = "Asics Lyte V",
                  Cost = "3990000",
                  Descrip = "This is so beautiful",
                  Image = "product-4.jpg",
                  Amount = "1",
                  IDKM = 4,
                  Size = "39",
                  IDLSP = 1
                },new Product
                {
                  Name = "Nike Jordan V",
                  Cost = "7890000",
                  Descrip = "This is so beautiful",
                  Image = "product-10.jpg",
                  Amount = "8",
                  IDKM = 3,
                  Size = "41",
                  IDLSP = 1
                },
            });

            context.SaveChanges();
        }
    }
}
