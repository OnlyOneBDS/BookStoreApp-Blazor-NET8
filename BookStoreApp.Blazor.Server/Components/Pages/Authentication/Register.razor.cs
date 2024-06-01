using BookStoreApp.Blazor.Server.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.Components.Pages.Authentication;

public partial class Register
{
  [Inject] private IClient HttpClient { get; set; } = default!;

  private readonly RegisterDto RegisterModel = new() { Role = "User" };

  private string message = string.Empty;

  private async Task HandleRegistration()
  {
    try
    {
      await HttpClient.RegisterAsync(RegisterModel);
      NavigateToLogin();
    }
    catch (ApiException ex)
    {
      if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
      {
        NavigateToLogin();
      }

      message = ex.Response;
    }
  }

  private void NavigateToLogin()
  {
    NavigationManager.NavigateTo("/login");
  }
}