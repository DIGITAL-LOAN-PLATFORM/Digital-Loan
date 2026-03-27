using Application.Interface;
using Application.Interfaces;
using Application.Service;
using Application.Services;
using Application.Services.Borrowers;

using Application.Services.LoanApplications;

using Application.Services.Locations;

using Infrastructure.Data;
using Infrastructure.DependencyInjection;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();// MudBlazor Services

// Add HttpContext accessor for server-side operations
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

    // add controllers for acount endpoints (login/logout)

    builder.Services.AddControllers();

// Application layer services
builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<IGuarantorService, GuarantorService>();
builder.Services.AddScoped<IGuarantorTypeService, GuarantorTypeService>();
builder.Services.AddScoped<IPaymentModalityService, PaymentModalityService>();
builder.Services.AddScoped<ILoanProductService, LoanProductService>();
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILoanDisbursementService, LoanDisbursementService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

// / Depedency Injection for infrastructure Layer
builder.Services.AddInfrastructureServices(builder.Configuration);

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
app.MapStaticAssets();  // For wwwroot assets
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();



app.Run();
