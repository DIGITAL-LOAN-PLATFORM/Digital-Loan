using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Infrastructure.Data;
using Infrastructure.Identity;
using Application.Interface;
using Infrastructure.Repository;
using Infrastructure.Repositories;
using System;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2. Identity Configuration
            // Note: AddIdentity internally calls AddAuthentication and registers the "Identity.Application" scheme.
            services.AddIdentity<User, IdentityRole<int>>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // 3. Configure Identity Cookie Settings
            // We use ConfigureApplicationCookie to MODIFY the existing scheme instead of adding a new one.
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "DigitalLoan.Identity";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/access-denied";
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            // 4. Token Lifespan for Password Reset/Email Confirmation
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(6);
            });

            // 5. Custom Claims Factory
            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomUserClaimsPrincipalFactory>();

            // 6. Register Repositories (Domain/Infrastructure Interface Mapping)
            services.AddScoped<IBorrower, BorrowerRepository>();
            services.AddScoped<IGuarantorType, GuarantorTypeRepository>();
            services.AddScoped<IPaymentModality, PaymentModalityRepository>();
            services.AddScoped<ILoanProduct, LoanProductRepository>();
            services.AddScoped<IGuarantor, GuarantorRepository>();
            services.AddScoped<ILoanApplication, LoanApplicationRepository>();
            services.AddScoped<ILoanDisbursement, LoanDisbursementRepository>();
            
            // 7. Identity Service Implementation
            services.AddScoped<IIdentity, IdentityRepository>();

            // 8. Miscellaneous
            services.AddHttpContextAccessor();

            return services;
        }
    }
}