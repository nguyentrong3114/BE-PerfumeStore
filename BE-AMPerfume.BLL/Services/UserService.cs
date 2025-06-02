using AutoMapper;
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Interfaces;
using System.Security.Cryptography;
using System.Text;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
    {
        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
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
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.UserRepository.CreateAsync(newUser);

        var otp = new Random().Next(100000, 999999).ToString();
        await _emailService.SaveOtpAsync(dto.Email, otp);
        await _emailService.SendVerificationCodeAsync(dto.Email, dto.Name, otp);
        await _unitOfWork.SaveChangesAsync();
        return new AuthResponseDTO
        {
            IsSuccess = true,
            Message = "Đăng ký thành công. Vui lòng kiểm tra email để xác minh tài khoản."
        };
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }


    public async Task<bool?> ChangePasswordAsync(string email, ChangePasswordDTO dto)
    {
        string oldPasswordHash = HashPassword(dto.OldPassword);
        string newPasswordHash = HashPassword(dto.NewPassword);

        return await _unitOfWork.UserRepository.UpdatePasswordAsync(email, oldPasswordHash, newPasswordHash);
    }
    public async Task<bool> MarkUserAsVerifiedAsync(string email)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
        if (user == null || user.IsVerify) return false;

        user.IsVerify = true;
        user.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.UserRepository.UpdateAsync(user);
        return true;
    }

    public Task<UserDTO> GetUserAsync(string email)
    {
        return _unitOfWork.UserRepository.GetUserAsync(email)
            .ContinueWith(task => _mapper.Map<UserDTO>(task.Result));
    }

    public async Task<PagedResult<UserDTO>> GetAllAsync(int page, int size)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        var dtos = _mapper.Map<List<UserDTO>>(users);

        return new PagedResult<UserDTO>
        {
            Items = dtos,
            PageNumber = page,
            PageSize = size,
            TotalItems = users.Count()
        };
    }
}
