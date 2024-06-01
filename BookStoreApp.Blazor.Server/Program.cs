using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Components;
using BookStoreApp.Blazor.Server.Providers;
using BookStoreApp.Blazor.Server.Services.Authentication;
using BookStoreApp.Blazor.Server.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
  .AddRazorComponents()
  .AddInteractiveServerComponents();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient<IClient, Client>(httpClient => httpClient.BaseAddress = new Uri("https://localhost:7107"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(auth => auth.GetRequiredService<AuthStateProvider>());

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

app
  .MapRazorComponents<App>()
  .AddInteractiveServerRenderMode();

app.Run();