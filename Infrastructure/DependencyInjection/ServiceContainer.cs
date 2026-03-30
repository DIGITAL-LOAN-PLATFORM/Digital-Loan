using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Application.Interface;
using Application.Interfaces;
using Infrastructure.Repository;
using Domain.Interface;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        // Renamed to AddInfrastructureServices (Plural) to match Program.cs standard
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Register DbContext with SplitQuery to resolve the performance warning
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            // 2. Identity Configuration
            services.AddIdentity<User, IdentityRole<int>>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // 3. Cookie Settings
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "DigitalLoan.Identity";
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/access-denied";
            });

            // 4. Repositories Mapping
            services.AddScoped<IAccount, AccountRepository>();
            services.AddScoped<IBorrower, BorrowerRepository>();
            services.AddScoped<IGuarantor, GuarantorRepository>();
            services.AddScoped<IGuarantorType, GuarantorTypeRepository>();
            services.AddScoped<ILoanApplication, LoanApplicationRepository>();
            services.AddScoped<ILoanDisbursement, LoanDisbursementRepository>();
            services.AddScoped<ILoanProduct, LoanProductRepository>();
            services.AddScoped<IPayment, PaymentRepository>();
            services.AddScoped<IPaymentModality, PaymentModalityRepository>();
            services.AddScoped<IPaymentType, PaymentTypeRepository>();
            services.AddScoped<IPenalty, PenaltyRepository>();
            services.AddScoped<IReason, ReasonRepository>();
            services.AddScoped<IRequiredDocument, RequiredDocumentRepository>();
            services.AddScoped<ILoanRequirements, LoanRequirementsRepository>();
            services.AddScoped<IIdentity, IdentityRepository>();
            services.AddScoped<IProcessFeeDeposit, ProcessFeeDepositRepository>();
            services.AddScoped<IProvidedDocument, ProvidedDocumentRepository>();
            

            services.AddHttpContextAccessor();

            return services;
        }
    }
}