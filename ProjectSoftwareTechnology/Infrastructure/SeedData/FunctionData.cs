
using Domain.Entities;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class FunctionData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Functions.Any()) return;
            context.Functions.AddRange(new List<Function> {
                new Function
                {
                    Name = "Product Management",
                    NameMethod = "ProductManagement"
                },
                new Function
                {
                   Name = "Brand Management",
                   NameMethod = "ProductTypeManagement"
                },
                new Function
                {
                    Name = "Inventory Management",
                    NameMethod = "TheFirstInventoryManagement"
                },
                new Function
                {
                   Name = "Order Management",
                   NameMethod = "TheFirstStatus"
                },
                new Function
                {
                    Name = "Account Management",
                    NameMethod = "UserManagement"
                },
                new Function
                {
                    Name = "Shipping Fee Management",
                    NameMethod = "ShippingFeeManagement"
                },
                new Function
                {
                    Name = "Statistical",
                    NameMethod = "Turnover"
                }
            });

            context.SaveChanges();
        }
    }
}
