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
    private readonly IEmailService _emailService;

    public UserService(IUserRepository repository, IMapper mapper, IEmailService emailService)
    {
        _repository = repository;
        _mapper = mapper;
        _emailService = emailService;
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

        if (string.IsNullOrWhiteSpace(dto.Password))
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
            IsVerify = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(newUser);

        var otp = new Random().Next(100000, 999999).ToString();
        await _emailService.SaveOtpAsync(dto.Email, otp);
        await _emailService.SendVerificationCodeAsync(dto.Email, dto.Name, otp);

        return new AuthResponseDTO
        {
            IsSuccess = true,
            Message = "Đăng ký thành công. Vui lòng kiểm tra email để xác minh tài khoản."
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

    public async Task<bool?> ChangePasswordAsync(string email, ChangePasswordDTO dto)
    {
        string oldPasswordHash = HashPassword(dto.OldPassword);
        string newPasswordHash = HashPassword(dto.NewPassword);

        return await _repository.UpdatePasswordAsync(email, oldPasswordHash, newPasswordHash);
    }
    public async Task<bool> MarkUserAsVerifiedAsync(string email)
    {
        var user = await _repository.GetByEmailAsync(email);
        if (user == null || user.IsVerify) return false;

        user.IsVerify = true;
        user.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(user);
        return true;
    }

}
