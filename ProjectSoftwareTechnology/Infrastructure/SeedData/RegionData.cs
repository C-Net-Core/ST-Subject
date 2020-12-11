
using Domain.Entities;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class RegionData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Regions.Any()) return;
            context.Regions.AddRange(new List<Region> {
                new Region
                {
                    NameArea = "Khác",
                    FeeShip = 14900
                },
                new Region
                {
                    NameArea = "Miền Nam",
                    FeeShip = 11900
                },
                new Region
                {
                    NameArea = "Miền Trung",
                    FeeShip = 16900
                },
                new Region
                {
                    NameArea = "Miền Bắc",
                    FeeShip = 21900
                }
            });

            context.SaveChanges();
        }
    }
}
