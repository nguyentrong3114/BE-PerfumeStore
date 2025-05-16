namespace BE_AMPerfume.DAL.Interfaces;

using BE_AMPerfume.Core.DTOs;

public interface IUserService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
    Task<IEnumerable<UserDTO>> GetAllAsync();
}
