using BookStoreApp.Blazor.Server.Services.Authentication;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.Components.Pages.Authentication;

public partial class Logout
{
  [Inject] IAuthService AuthService { get; set; } = default!;

  protected override async Task OnInitializedAsync()
  {
    await AuthService.Logout();
    NavigationManager.NavigateTo("/");
  }
}