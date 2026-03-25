using Application.Interface;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Application.Services.Locations;

using Domain.Entities;
using Infrastructure.Repository;
using Application.Interfaces;


namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(
            this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DigitalLoanMSSQLConnection")),
                    ServiceLifetime.Scoped);

<<<<<<< HEAD
            // Repositories — only add ones that actually exist
            services.AddScoped<IRequiredDocument,  RequiredDocumentRepository>();
            services.AddScoped<IDocumentType,       DocumentTypeRepository>();
            services.AddScoped<IBorrower,           BorrowerRepository>();
             services.AddScoped<IProvidedDocument,           ProvidedDocumentRepository>();
=======
            // // Register identity service
            // services.AddAuthenticationService(configuration);

            // // Register User Context (for accessing current user anywhere)
            // services.AddHttpContextAccessor();
            // services.AddScoped<IUserContext, UserContext>();
 
                // Register repositories
                services.AddScoped<IBorrower, BorrowerRepository>();
                services.AddScoped<IGuarantorType, GuarantorTypeRepository>();
                services.AddScoped<IPaymentModality, PaymentModalityRepository>();
                services.AddScoped<ILoanProduct, LoanProductRepository>();
                services.AddScoped<IGuarantor, GuarantorRepository>();
                services.AddScoped<ILoanApplication, LoanApplicationRepository>();
                services.AddScoped<global::Application.Interface.ILocationService, global::Application.Services.Locations.LocationService>();
                // ILocationService directly registered via app services (in Program.cs)
               

            // Register Repositories 
            services.AddScoped<IRequiredDocument, RequiredDocumentRepository>();
            // services.AddScoped<IGuest, GuestRepository>();
            // services.AddScoped<IIdentity, IdentityRepository>();

            // Register Data Seeder
            // services.AddScoped<IDataSeeder, DataSeeder>();
>>>>>>> fb620d7d9a732b664595c86cf34c364f450eb3de

            return services;
        }
    }
}