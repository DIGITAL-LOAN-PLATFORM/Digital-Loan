using Web.Components;
using MudBlazor.Services;
using Application.Services.RequiredDocuments;
using Infrastructure.DependencyInjection; 
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructureService(builder.Configuration); 
// builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IRequiredDocumentService, RequiredDocumentService>();

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