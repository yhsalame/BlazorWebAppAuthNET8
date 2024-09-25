using Blazored.LocalStorage;
using BlazorWebAppAuthNET8.Components;
using BlazorWebAppAuthNET8.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//----- Register Service Here -----

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "CustomScheme";
    options.DefaultChallengeScheme = "CustomScheme";
})
.AddScheme<CustomAuthOptions, CustomAuthenticationHandler>("CustomScheme", options => { });

builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddHttpClient();
//---------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
