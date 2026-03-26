using System;
using System.IO;
using System.Text.Json;
using Web.Components;
using MudBlazor.Services;
using Application.Services.RequiredDocuments;
using Application.Services.ProvidedDocuments;
using Application.Services.DocumentTypes;
using Application.Services.Borrowers;      
using Application.Services;               
using Infrastructure.DependencyInjection;
using Application.Interface;
using Infrastructure.Services;
using Infrastructure.DependencyInjection; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Services;
using Application.Interface;
using Application.Services.Locations;
using Application.Services.Borrowers;
using Application.Services.Payments;
using Application.Services.PaymentTypes;
using Application.Services.Reasons;
using Application.Services.Penalties;


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



// Location Service
builder.Services.AddSingleton<ILocationService>(sp =>
{
    var env    = sp.GetRequiredService<IWebHostEnvironment>();
    var logger = sp.GetRequiredService<ILogger<JsonLocationService>>();
    return new JsonLocationService(env.WebRootFileProvider, logger);
});

// Application Services
    builder.Services.AddMudServices();


//infrastructure services
builder.Services.AddInfrastructureService(builder.Configuration);

//application services
builder.Services.AddScoped<IAccountService, AccountService>();





// builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IRequiredDocumentService, RequiredDocumentService>();
builder.Services.AddScoped<IDocumentTypeService,     DocumentTypeService>();
builder.Services.AddScoped<IBorrowerService,         BorrowerService>();
builder.Services.AddScoped<IProvidedDocumentService, ProvidedDocumentService>();
    // add controllers for acount endpoints (login/logout)

    builder.Services.AddControllers();

    builder.Services.AddScoped<IBorrowerService, BorrowerService>();
    builder.Services.AddScoped<IGuarantorTypeService, GuarantorTypeService>();
    builder.Services.AddScoped<IPaymentModalityService, PaymentModalityService>();
    builder.Services.AddScoped<ILoanProductService, LoanProductService>();
    builder.Services.AddScoped<IGuarantorService, GuarantorService>();
    builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
    builder.Services.AddScoped<IReasonService, ReasonService>();
    builder.Services.AddScoped<IPenaltyService, PenaltyService>();
    
    // LocationService registered using factory - resolves namespace issue
    // LocationService now properly registered via ServiceContainer
    
    // ILocationService registered - file loader, no deps

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddControllers();

var app = builder.Build();

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
