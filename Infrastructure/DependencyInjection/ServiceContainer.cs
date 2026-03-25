using Application.Interface;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            // Repositories — only add ones that actually exist
            services.AddScoped<IRequiredDocument,  RequiredDocumentRepository>();
            services.AddScoped<IDocumentType,       DocumentTypeRepository>();
            services.AddScoped<IBorrower,           BorrowerRepository>();
             services.AddScoped<IProvidedDocument,           ProvidedDocumentRepository>();

            return services;
        }
    }
}