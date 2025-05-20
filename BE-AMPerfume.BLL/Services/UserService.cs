using AutoMapper;
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Interfaces;
using System.Security.Cryptography;
using System.Text;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
    {
        var existingUser = await _repository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return new AuthResponseDTO
            {
                IsSuccess = false,
                Message = "Email đã được sử dụng"
            };
        }
        if (dto.Password == null)
        {
            return new AuthResponseDTO
            {
                IsSuccess = false,
                Message = "Thiếu mật khẩu"
            };
        }
        if (dto.Password.Length < 6)
        {
            return new AuthResponseDTO
            {
                IsSuccess = false,
                Message = "Mật khẩu phải có ít nhất 6 ký tự"
            };
        }
        var newUser = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(newUser);
        return new AuthResponseDTO
        {
            IsSuccess = true,
            Message = "Đăng ký thành công"
        };
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public async Task<UserDTO?> GetUserAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        var existingUser = await _repository.GetByEmailAsync(email);
        if (existingUser == null)
            return null;

        return new UserDTO
        {
            Email = existingUser.Email,
            FullName = existingUser.Name,
            Address = existingUser.Address,
            CreatedAt = existingUser.CreatedAt,
            UpdatedAt = existingUser.UpdatedAt,
        };
    }

    public async Task<bool?> ChangePasswordAsync(string email,ChangePasswordDTO dto)
    {
        string oldPasswordHash = HashPassword(dto.OldPassword);
        string newPasswordHash = HashPassword(dto.NewPassword);

        return await _repository.UpdatePasswordAsync(email, oldPasswordHash, newPasswordHash);
    }
}
