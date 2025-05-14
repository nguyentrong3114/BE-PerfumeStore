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

    public async Task<bool> RegisterAsync(RegisterDTO dto)
    {
        var existingUser = await _repository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            return false;

        var newUser = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(newUser);
        return true;
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
}
