
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
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DigitalLoanMSSQLConnection")));
            
            //Register identity services
            

            //Register Repositories
           
            services.AddScoped<IAccount , AccountRepository>();
          
            // Add infrastructure services here, e.g., DbContext, Repositories, etc.
            
            );

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
                services.AddScoped<IPayment, PaymentRepository>();
                services.AddScoped<IPaymentType, PaymentTypeRepository>();
                services.AddScoped<IReason, ReasonRepository>();
                services.AddScoped<IPenalty, PenaltyRepository>();

            // Register Repositories 
            services.AddScoped<IRequiredDocument, RequiredDocumentRepository>();
            // services.AddScoped<IGuest, GuestRepository>();
            // services.AddScoped<IIdentity, IdentityRepository>();

            // Register Data Seeder
            // services.AddScoped<IDataSeeder, DataSeeder>();


            return services;
        }
    }
}
