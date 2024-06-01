using BookStoreApp.Blazor.Server.Services.Authentication;
using BookStoreApp.Blazor.Server.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.Components.Pages.Authentication;

public partial class Login
{
  [Inject] private IAuthService AuthService { get; set; } = default!;

  private readonly LoginDto LoginModel = new();

  private string message = string.Empty;

  private async Task HandleLogin()
  {
		try
		{
      var response = await AuthService.AuthenticateAsync(LoginModel);

      if (response)
      {
        NavigationManager.NavigateTo("/");
      }

      message = "Invalid credentials, please try again";
    }
		catch (ApiException ex)
		{
      if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
      {
        
      }

      message = ex.Response;
    }
  }
}