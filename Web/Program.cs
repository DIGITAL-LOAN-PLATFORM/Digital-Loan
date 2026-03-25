using Web.Components;
using MudBlazor.Services;
using Application.Services.RequiredDocuments;
<<<<<<< HEAD
using Application.Services.ProvidedDocuments;
using Application.Services.DocumentTypes;
using Application.Services.Borrowers;      
using Application.Services;               
using Infrastructure.DependencyInjection;
using Application.Interface;
using Infrastructure.Services;
=======
using Infrastructure.DependencyInjection; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Services;
using Application.Interface;
using Application.Services.Locations;
using Application.Services.Borrowers;
using System;
using System.IO;
using System.Text.Json;
using Application.Interfaces;
>>>>>>> fb620d7d9a732b664595c86cf34c364f450eb3de

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registers DbContext + all repositories
builder.Services.AddInfrastructureService(builder.Configuration);

// Location Service
builder.Services.AddSingleton<ILocationService>(sp =>
{
    var env    = sp.GetRequiredService<IWebHostEnvironment>();
    var logger = sp.GetRequiredService<ILogger<JsonLocationService>>();
    return new JsonLocationService(env.WebRootFileProvider, logger);
});

// Application Services
builder.Services.AddScoped<IRequiredDocumentService, RequiredDocumentService>();
<<<<<<< HEAD
builder.Services.AddScoped<IDocumentTypeService,     DocumentTypeService>();
builder.Services.AddScoped<IBorrowerService,         BorrowerService>();
builder.Services.AddScoped<IProvidedDocumentService, ProvidedDocumentService>();
=======
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
>>>>>>> fb620d7d9a732b664595c86cf34c364f450eb3de

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
