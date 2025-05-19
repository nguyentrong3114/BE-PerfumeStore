using System.Security.Claims;
using BE_AMPerfume.Core.DTOs;

public interface IAuthService
{

    Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto);
    Task<AuthResponseDTO?> IsLogin(string email);
    Task<string> HandleExternalLoginAsync(ClaimsPrincipal principal);
    
}
 