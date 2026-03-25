using Web.Components;
using MudBlazor.Services;
<<<<<<< HEAD
=======
using Application.Services.RequiredDocuments;
using Infrastructure.DependencyInjection; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
>>>>>>> 8b26a51bdf784e04111b2993293fca12b6772dcf
using Domain.Entities;
using Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Application.Interfaces;
using Infrastructure.Repositories;
using Application.Services.Accounts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

<<<<<<< HEAD
    builder.Services.AddMudServices();


//infrastructure services
builder.Services.AddInfrastructureService(builder.Configuration);

//application services
builder.Services.AddScoped<IAccountService, AccountService>();




=======
builder.Services.AddInfrastructureService(builder.Configuration); 
// builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IRequiredDocumentService, RequiredDocumentService>();
>>>>>>> 8b26a51bdf784e04111b2993293fca12b6772dcf
    // add controllers for acount endpoints (login/logout)

    builder.Services.AddControllers();

    builder.Services.AddScoped<IBorrowerService, BorrowerService>();
    builder.Services.AddScoped<IGuarantorTypeService, GuarantorTypeService>();
    builder.Services.AddScoped<IPaymentModalityService, PaymentModalityService>();
    builder.Services.AddScoped<ILoanProductService, LoanProductService>();
    builder.Services.AddScoped<IGuarantorService, GuarantorService>();
    builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
    
    // LocationService registered using factory - resolves namespace issue
    // LocationService now properly registered via ServiceContainer
    
    // ILocationService registered - file loader, no deps

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddControllers();

var app = builder.Build();

// Run seeder
// using (var scope = app.Services.CreateScope())
// {
//     var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
//     await seeder.SeedAsync();
// }

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapControllers();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
