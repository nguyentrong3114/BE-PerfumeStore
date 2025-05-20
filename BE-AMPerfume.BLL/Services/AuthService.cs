using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BE_AMPerfume.BLL.Helpers;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Interfaces;

public class AuthService : IAuthService
{
    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthService(JwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto)
    {
        var hashedPassword = HashPassword(loginDto.Password);
        var user = await _userRepository.GetByEmailAndPasswordHashAsync(loginDto.Email, hashedPassword);

        if (user == null) return null;

        var token = _tokenGenerator.GenerateToken(user.Email, user.Name, user.Id);
        return new AuthResponseDTO
        {
            FullName = user.Name,
            Email = user.Email,
            Token = token,
        };
    }

    public async Task<AuthResponseDTO?> IsLogin(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return null;

        return new AuthResponseDTO
        {
            Email = user.Email,
            FullName = user.Name,
        };
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public async Task<string> HandleExternalLoginAsync(ClaimsPrincipal principal)
    {
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        var name = principal.Identity?.Name;

        if (string.IsNullOrEmpty(email))
            throw new Exception("Email claim not found.");

        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            user = new User
            {
                Email = email,
                Name = name ?? "",
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.CreateAsync(user);
        }

        var token = _tokenGenerator.GenerateToken(user.Email, user.Name, user.Id);
        return token;
    }

}
