using BE_AMPerfume.Core.DTOs;

public interface IAuthService
{
    
    Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto);
    Task<AuthResponseDTO?> IsLogin(string email);

}
