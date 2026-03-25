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
builder.Services.AddScoped<IDocumentTypeService,     DocumentTypeService>();
builder.Services.AddScoped<IBorrowerService,         BorrowerService>();
builder.Services.AddScoped<IProvidedDocumentService, ProvidedDocumentService>();

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
