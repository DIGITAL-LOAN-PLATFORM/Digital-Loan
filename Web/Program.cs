using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Web.Components;
using Infrastructure.Data;
using Infrastructure.DependencyInjection;
using Application.Interface;
using Application.Interfaces;
using Application.Services;
// ... other usings ...
using Hangfire;
using Application.Services.RequiredDocuments;
using Application.Services.Reasons;
using Application.Services.ProcessFeeDeposits;
using Application.Services.PaymentTypes;
using Application.Services.LoanApplications;
using Application.Services.Borrowers;
using Application.Services.Accounts;
using Application.Services.ProvidedDocuments;
using Application.Services.LoanRequirements;
using Application.Services.Locations;

var builder = WebApplication.CreateBuilder(args);

// --- UI & Core Services ---
builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddControllers();

// --- Application Layer Services ---
// ... (Your existing Scoped services) ...
builder.Services.AddScoped<IPenaltyService, PenaltyService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ILoanDisbursementService, LoanDisbursementService>();
builder.Services.AddScoped<IRequiredDocumentService, RequiredDocumentService>();
builder.Services.AddScoped<IReasonService, ReasonService>();
builder.Services.AddScoped<IProcessFeeDepositService, ProcessFeeDepositService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IPaymentModalityService, PaymentModalityService>();
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
builder.Services.AddScoped<IGuarantorTypeService, GuarantorTypeService>();
builder.Services.AddScoped<IGuarantorService, GuarantorService>();
builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoanProductService, LoanProductService>();
builder.Services.AddScoped<IProvidedDocumentService, ProvidedDocumentService>();
builder.Services.AddScoped<ILoanRequirementsService, LoanRequirementsService>();
builder.Services.AddScoped<ILocationService, LocationService>();

// --- Infrastructure Layer ---
builder.Services.AddInfrastructureServices(builder.Configuration);

// 2. Add Hangfire Services
// This configures Hangfire to use your SQL Server connection string
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Add the Hangfire Server (the background worker)
builder.Services.AddHangfireServer();

builder.Services.AddAuthorization();

var app = builder.Build();

// --- Database Migrations ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();

        // 4. Schedule the Penalty Job
        // We do this inside a scope to ensure IPenaltyService can be resolved
        var penaltyService = services.GetRequiredService<IPenaltyService>();
        
        // This sets the job to run daily at Midnight (00:00)
        // It will trigger your 5% "Day 1" penalty logic automatically
        RecurringJob.AddOrUpdate(
            "daily-penalty-check", 
            () => penaltyService.ApplyDayOnePenaltiesAsync(), 
            Cron.Daily);
    }
    catch (Exception ex) 
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during startup.");
    }
}

// --- Middleware Pipeline ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// 5. Add Hangfire Dashboard (Optional: lets you see jobs at /hangfire)
app.UseHangfireDashboard();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();