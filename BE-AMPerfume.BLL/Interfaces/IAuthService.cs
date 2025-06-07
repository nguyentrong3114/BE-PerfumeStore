using System.Security.Claims;
using BE_AMPerfume.Core.DTOs;

public interface IAuthService
{

    Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto);
    Task<AuthResponseDTO?> IsLogin(string email);
    Task<string> HandleExternalLoginAsync(ClaimsPrincipal principal);
    Task<bool> SendOtpToEmailAsync(string email);
    Task<bool> VerifyOtpAsync(string email, string otp);
    Task<bool> ResetPasswordAsync(string email, string newPassword, string otp);
}
