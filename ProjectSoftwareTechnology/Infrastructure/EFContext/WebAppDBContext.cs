
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFContext
{
    public class WebAppDBContext : DbContext
    {
        public WebAppDBContext(DbContextOptions<WebAppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(s => s.ID);
            modelBuilder.Entity<Admin>().HasKey(s => s.ID);
            modelBuilder.Entity<ProductType>().HasKey(s => s.IDLSP);
            modelBuilder.Entity<Sale>().HasKey(s => s.IDKM);
            modelBuilder.Entity<InfoOrder>().HasKey(s =>
                new
                {
                    s.IDP,
                    s.IDSP
                });
            modelBuilder.Entity<ProductHasDeleted>().HasKey(s => s.ID);
            modelBuilder.Entity<PayOrder>().HasKey(s => s.ID);
            modelBuilder.Entity<PayOrderCancel>().HasKey(s => s.ID);
            modelBuilder.Entity<Product>().HasKey(s => s.IDSP);
            modelBuilder.Entity<Region>().HasKey(s => s.IdRegion);
            modelBuilder.Entity<Province>().HasKey(s => s.IdProvince);
            modelBuilder.Entity<Ward>().HasKey(s => s.IdWard);
            modelBuilder.Entity<PayOrder>().HasKey(s => s.ID);
            modelBuilder.Entity<InfoOrder>().HasKey(s =>
                new
                {
                    s.IDP,
                    s.IDSP
                });
            modelBuilder.Entity<Authorization>().HasKey(s =>
               new
               {
                   s.IDAdmin,
                   s.IDCN
               });
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<InfoOrder> InfoOrders { get; set; }
        public DbSet<PayOrder> PayOrders { get; set; }
        public DbSet<PayOrderCancel> PayOrderCancels { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductHasDeleted> ProductHasDeleteds { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Function> Functions { get; set; }
    }
}
