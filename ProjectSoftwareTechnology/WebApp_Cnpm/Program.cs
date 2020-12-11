
using Infrastructure.EFContext;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp_Cnpm
{
    public class Program
    {
        public static void Main()
        {
            var host = CreateHostBuilder();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<WebAppDBContext>();
                AccountData.Initialize(context);
                AdminData.Initialize(context);
                FunctionData.Initialize(context);
                ProductData.Initialize(context);
                ProductTypeData.Initialize(context);
                SaleData.Initialize(context);
                RegionData.Initialize(context);
                ProvinceData.Initialize(context);
                WardData.Initialize(context);
                AuthorData.Initialize(context);
            }

            host.Run();
        }

        private static IHost CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                        .ConfigureWebHostDefaults(builder =>
                        {
                            builder.UseStartup<Startup>();
                        })
                        .Build();
        }
    }
}
