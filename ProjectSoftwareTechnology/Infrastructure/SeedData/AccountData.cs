
using Domain.Entities;
using Infrastructure.EFContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SeedData
{
    public class AccountData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Accounts.Any()) return;
            context.Accounts.AddRange(new List<Account> {
                new Account
                {
                    UID = "lengothienan",
                    PW = "041f7b834b36b8d4f10e5a62b978550e",
                    Status = "On",
                    Address = "Tôn Thất Thuyết,Quận 4,TP.HCM ",
                    Email = "lengothienan9a2@gmail.com",
                    DateActive = DateTime.Now.ToString()
                },
                new Account
                {
                    UID = "huynhquangvinh",
                    PW = "36be7ec11d8c564863178836579c646f",
                    Status = "On",
                    Address = "Trần Hưng Đạo, Quận 5, TP.HCM",
                    Email = "huynhquangvinh01121999@gmail.com",
                    DateActive = DateTime.Now.ToString()
                },
                new Account
                {
                    UID = "nguyenngochoangbaotran",
                    PW = "4d8712e0063b99712e9d52da5c008625",
                    Status = "On",
                    Address = "Quận 8, TP.HCM",
                    Email = "trantran01225650365@gmail.com",
                    DateActive = DateTime.Now.ToString()
                },
                new Account
                {
                    UID = "nguyenhoanganh",
                    PW = "9b155c23d8a25b006221a052227dd7f9",
                    Status = "On",
                    Address = "Quận 6, TP.HCM",
                    Email = "nguyenhoanganh3118410011@gmail.com",
                    DateActive = DateTime.Now.ToString()
                },
                new Account
                {
                    UID = "nguyenthaivan",
                    PW = "e57f603b2942355c52f9f8af585e7f25",
                    Status = "On",
                    Address = "An Dương Vương, Quận 5, TP.HCM",
                    Email = "wvan3011@gmail.com",
                    DateActive = DateTime.Now.ToString()
                }
            });

            context.SaveChanges();
        }
    }
}
