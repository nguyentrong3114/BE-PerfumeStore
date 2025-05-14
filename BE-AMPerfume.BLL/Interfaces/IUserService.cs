namespace BE_AMPerfume.DAL.Interfaces;

using BE_AMPerfume.Core.DTOs;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterDTO registerDto);
    Task<IEnumerable<UserDTO>> GetAllAsync();
}
