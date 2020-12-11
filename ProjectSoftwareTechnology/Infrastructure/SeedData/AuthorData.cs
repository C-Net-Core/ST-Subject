using Domain.Entities;
using Infrastructure.EFContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.SeedData
{
    public class AuthorData
    {
        public static void Initialize(WebAppDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Authorizations.Any()) return;
            context.Authorizations.AddRange(new List<Authorization> {
                new Authorization
                {
                    IDAdmin = 4,
                    IDCN = 1
                },
                new Authorization
                {
                    IDAdmin = 4,
                    IDCN = 2
                },
                new Authorization
                {
                    IDAdmin = 4,
                    IDCN = 3
                },
                new Authorization
                {
                    IDAdmin = 4,
                    IDCN = 4
                },
                new Authorization
                {
                    IDAdmin = 4,
                    IDCN = 5
                },
                new Authorization
                {
                    IDAdmin = 4,
                    IDCN = 6
                },
                new Authorization
                {
                    IDAdmin = 4,
                    IDCN = 7
                }
            });

            context.SaveChanges();
        }
    }
}
