using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Data;
using BE_AMPerfume.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AMPerfumeDbContext _context;

    public UserRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetByEmailAndPasswordHashAsync(string email, string passwordHash)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash);
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UpdatePasswordAsync(string email, string passwordHash, string newPasswordHash)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return false;

        if (user.PasswordHash != passwordHash)
            return false;

        user.PasswordHash = newPasswordHash;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser == null)
            return false;

        // Update properties as needed
        existingUser.IsVerify = user.IsVerify;
        // Add other properties as necessary

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();
        return true;
    }
}
