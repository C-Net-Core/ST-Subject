
using Domain.Entities;
using Infrastructure.EFContext;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class ProvinceData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Provinces.Any()) return;
            context.Provinces.AddRange(new List<Province> {
                new Province
                {
                    NameProvince = "Cần Thơ",
                    IdReion = 4
                },
                new Province
                {
                    NameProvince = "Hồ Chí Minh",
                    IdReion = 3
                },
                new Province
                {
                    NameProvince = "Huế",
                    IdReion = 2
                },
                new Province
                {
                    NameProvince = "Hà Nội",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Vĩnh Phúc",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Bắc Ninh",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Hải Dương",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Hưng Yên",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Hà Nam",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Hòa Bình",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Phú Thọ",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Thái Nguyên",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Bắc Giang",
                    IdReion = 1
                },
                new Province
                {
                    NameProvince = "Quảng Ngãi",
                    IdReion = 2
                },
                new Province
                {
                    NameProvince = "KomTum",
                    IdReion = 2
                },
            });

            context.SaveChanges();
        }
    }
}
