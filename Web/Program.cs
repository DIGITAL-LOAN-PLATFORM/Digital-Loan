using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Web.Components;
// ADD THIS: Replace 'Infrastructure.DependencyInjection' with the actual 
// namespace where your ServiceContainer class lives


var builder = WebApplication.CreateBuilder(args);

// 1. Add Razor Components (UI)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 3. Register the DbContext for the System of Record [cite: 50]
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 4. Call your custom Infrastructure registration 
// This registers your Repositories and Identity logic
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();