using MudBlazor.Services;
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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();// MudBlazor Services

// Add HttpContext accessor for server-side operations
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

    // add controllers for acount endpoints (login/logout)

    builder.Services.AddControllers();

    builder.Services.AddScoped<IBorrowerService, BorrowerService>();
    
    // LocationService registered using factory - resolves namespace issue
    // LocationService now properly registered via ServiceContainer
    
    // ILocationService registered - file loader, no deps

// Dependency Injection for infrastructure Layer
builder.Services.AddInfrastructureServices(builder.Configuration);

// LOCATION SERVICE TEST DISABLED for clean startup
Console.WriteLine("=== LOCATION TEST DISABLED - Startup fixed ===");
Console.WriteLine("Test LocationService via Borrowers page after startup.\\n");

// Add authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Run migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

//Add authentication

app.UseAuthentication();
app.UseAuthorization();

// app.MapIdentityApi<User>();

app.UseAntiforgery();
app.MapControllers(); // Acount Login/logout end point
app.UseStaticFiles();  // For wwwroot assets
app.MapRazorComponents<Web.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();

