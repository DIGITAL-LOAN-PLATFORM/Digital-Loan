using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Infrastructure.Data;
using Application.Interface;
using Infrastructure.Repositories;

using Application.Services.Locations;

using Domain.Entities;
using Infrastructure.Repository;
using Application.Interfaces;


namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // add infrastructure services here, e.g., DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
                // Register the UserContext as a scoped service
                

 
                // Register repositories
                services.AddScoped<IBorrower, BorrowerRepository>();
                services.AddScoped<IGuarantorType, GuarantorTypeRepository>();
                services.AddScoped<IPaymentModality, PaymentModalityRepository>();
                services.AddScoped<ILoanProduct, LoanProductRepository>();
                services.AddScoped<IGuarantor, GuarantorRepository>();
                services.AddScoped<ILoanApplication, LoanApplicationRepository>();
                services.AddScoped<global::Application.Interface.ILocationService, global::Application.Services.Locations.LocationService>();
                // ILocationService directly registered via app services (in Program.cs)
               

                
               

            // Register other infrastructure services here

            return services;
        }
    }
}
