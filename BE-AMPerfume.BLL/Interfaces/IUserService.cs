namespace BE_AMPerfume.DAL.Interfaces;

using BE_AMPerfume.Core.DTOs;

public interface IUserService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);

    Task<UserDTO> GetUserAsync(string email);
    Task<PagedResult<UserDTO>> GetAllAsync(int page, int size);
    Task<bool?> ChangePasswordAsync(string email, ChangePasswordDTO changePasswordDTO);
    Task<bool> MarkUserAsVerifiedAsync(string email);
}
