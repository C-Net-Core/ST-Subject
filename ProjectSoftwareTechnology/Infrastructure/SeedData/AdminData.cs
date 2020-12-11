
using Domain.Entities;
using Infrastructure.EFContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class AdminData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Admins.Any()) return;
            context.Admins.AddRange(new List<Admin> {
                new Admin
                {
                    UID = "lengothienan_ad",
                    PW = "041f7b834b36b8d4f10e5a62b978550e",
                    Permission = "ADMIN",
                    DateActive = DateTime.Now.ToString(),
                    Status = "On"
                },
                new Admin
                {
                    UID = "huynhquangvinh_ad",
                    PW = "36be7ec11d8c564863178836579c646f",
                    Permission = "MANAGER",
                    DateActive = DateTime.Now.ToString(),
                    Status = "On"
                },
                new Admin
                {
                    UID = "nguyenngochoangbaotran_ad",
                    PW = "4d8712e0063b99712e9d52da5c008625",
                    Permission = "SALESTAFF",
                    DateActive = DateTime.Now.ToString(),
                    Status = "On"
                },
                new Admin
                {
                    UID = "nguyenhoanganh_ad",
                    PW = "9b155c23d8a25b006221a052227dd7f9",
                    Permission = "SALESTAFF",
                    DateActive = DateTime.Now.ToString(),
                    Status = "On"
                },
                new Admin
                {
                    UID = "nguyenthaivan_ad",
                    PW = "e57f603b2942355c52f9f8af585e7f25",
                    Permission = "STOCKSTAFF",
                    DateActive = DateTime.Now.ToString(),
                    Status = "On"
                }
            });

            context.SaveChanges();
        }
    }
}
