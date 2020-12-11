
using Domain.Entities;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class WardData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Wards.Any()) return;
            context.Wards.AddRange(new List<Ward> {
                new Ward
                {
                    NameWard = "Quận 1",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 2",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 3",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 4",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 5",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 6",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 7",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 8",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 9",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 10",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 11",
                    IdProvince = 13
                },
                new Ward
                {
                    NameWard = "Quận 12",
                    IdProvince = 13
                },
            });

            context.SaveChanges();
        }
    }
}
