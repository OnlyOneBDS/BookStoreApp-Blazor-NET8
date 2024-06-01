using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.Providers;
using BookStoreApp.Blazor.Server.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.Services.Authentication;

public interface IAuthService
{
  Task<bool> AuthenticateAsync(LoginDto request);
  Task Logout();
}

public class AuthService : IAuthService
{
  private readonly IClient _httpClient;
  private readonly ILocalStorageService _localStorage;
  private readonly AuthenticationStateProvider _authStateProvider;

  public AuthService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
  {
    _httpClient = httpClient;
    _localStorage = localStorage;
    _authStateProvider = authStateProvider;
  }

  public async Task<bool> AuthenticateAsync(LoginDto request)
  {
    var response = await _httpClient.LoginAsync(request);

    // Store the token
    await _localStorage.SetItemAsync("authToken", response.Token);

    // Change auth state of app
    await ((AuthStateProvider)_authStateProvider).LoggedIn();

    return true;
  }

  public async Task Logout()
  {
    await ((AuthStateProvider)_authStateProvider).LoggedOut();
  }
}
