
using Domain.Entities;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class SaleData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Sales.Any()) return;
            context.Sales.AddRange(new List<Sale> {
                new Sale
                {
                    phantram = "90"
                },
                new Sale
                {
                    phantram = "80"
                },
                new Sale
                {
                    phantram = "70"
                },
                new Sale
                {
                    phantram = "60"
                },
                new Sale
                {
                    phantram = "50"
                },
                new Sale
                {
                    phantram = "40"
                },
                new Sale
                {
                    phantram = "30"
                },
                new Sale
                {
                    phantram = "20"
                },
                new Sale
                {
                    phantram = "10"
                },
                new Sale
                {
                    phantram = "0"
                },
            });

            context.SaveChanges();
        }
    }
}
