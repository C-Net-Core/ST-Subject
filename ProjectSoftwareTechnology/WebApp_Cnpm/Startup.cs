using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFContext;
using Domain.Repositories;
using Infrastructure.Repository;
using Domain.Repositories.IEFRepository;
using Infrastructure.EFRepository;
using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using WebApp_Cnpm.AuthorCustomRequire;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp_Cnpm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add DBContext
            services.AddDbContext<WebAppDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("myConnectionString"))
                //options.UseSqlite(Configuration.GetConnectionString("Default"))
            );
            #endregion

            #region Add Authentication Cookie

            #region Cookie Normal
            services.AddAuthentication("Cookie")
                .AddCookie("Cookie", config =>
                {
                    config.Cookie.Name = "Cookie";
                    config.LoginPath = "/Home/CheckLogin";
                });
            #endregion

            #region Cookie OAuthor
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/author/google-login"; // Must be lowercase
                options.LoginPath = "/author/facebook-login"; // Must be lowercase
            }).AddGoogle(options =>
            {
                options.ClientId = "649848807632-9ta0l2tas9uofcgjb5md42ici94n62sn.apps.googleusercontent.com";
                options.ClientSecret = "O88HYKuG_73WWmjTjuogwr_k";
            }).AddFacebook(options =>
            {
                options.AppId = "2868902503433039";
                options.AppSecret = "fb8abfb73304d8826c1862fe4b8abe0c";
            });
            #endregion

            #endregion

            #region Add Policy Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Admin"));

                options.AddPolicy("Claim.DoB", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                });
            });
            #endregion

            #region Add Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            #endregion

            #region AddScoped
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminDTOService, AdminService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountDTOService, AccountService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductDTOService, ProductService>();

            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleDTOService, SaleService>();

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductTypeDTOService, ProductTypeService>();

            services.AddScoped<IProductHasDeletedRepository, ProductHasDeletedRepository>();
            services.AddScoped<IProductHasDeletedDTOService, ProductHasDeletedService>();

            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionDTOService, RegionService>();

            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceDTOService, ProvinceService>();

            services.AddScoped<IWardRepository, WardRepository>();
            services.AddScoped<IWardDTOService, WardService>();

            services.AddScoped<IPayOrderRepository, PayOrderRepository>();
            services.AddScoped<IPayOrderDTOService, PayOrderService>();

            services.AddScoped<IPayOrderCancelRepository, PayOrderCancelRepository>();
            services.AddScoped<IPayOrderCancelDTOService, PayOrderCancelService>();

            services.AddScoped<IInfoOrderRepository, InfoOrderRepository>();
            services.AddScoped<IInfoOrderDTOService, InfoOrderService>();

            services.AddScoped<IFunctionRepository, FunctionRepository>();
            services.AddScoped<IFunctionDTOService, FunctionService>();

            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
            services.AddScoped<IAuthorizationDTOService, AuthorService>();

            #endregion

            #region Add Orther
            services.AddControllersWithViews();
            //services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddDistributedMemoryCache();
            #endregion

            #region Add AccessDenined
            services.ConfigureApplicationCookie(option =>
           {
               option.AccessDeniedPath = new PathString("/Account/AccessDenied");
           });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // who are you?
            app.UseAuthentication();

            // are you allowed?
            app.UseAuthorization();

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvcWithDefaultRoute();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
