namespace BE_AMPerfume.DAL.Interfaces;

using BE_AMPerfume.Core.DTOs;

public interface IUserService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);

    Task<UserDTO> GetUserAsync(string email);
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<bool?> ChangePasswordAsync(string email,ChangePasswordDTO changePasswordDTO);
}
