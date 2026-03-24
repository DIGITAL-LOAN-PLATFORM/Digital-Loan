using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;


namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DigitalLoanMSSQLConnection")));
            
            //Register identity services
            

            //Register Repositories
           
            services.AddScoped<IAccount , AccountRepository>();
          

            return services;
        }
    }
}