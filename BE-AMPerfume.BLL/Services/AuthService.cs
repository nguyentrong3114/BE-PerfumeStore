using System.Security.Cryptography;
using System.Text;
using BE_AMPerfume.BLL.Helpers;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Data;
using BE_AMPerfume.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
    private readonly AMPerfumeDbContext _context;
    private readonly JwtTokenGenerator _tokenGenerator;
    public AuthService(AMPerfumeDbContext context, JwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
        _context = context;
    }


    public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null || user.PasswordHash != HashPassword(loginDto.Password))
            return null;
        var token = _tokenGenerator.GenerateToken(user.Email, user.Name);
        return new AuthResponseDTO
        {
            FullName = user.Name,
            Email = user.Email,
            Token = token,
        };
    }

    public async Task<AuthResponseDTO?> IsLogin(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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


}