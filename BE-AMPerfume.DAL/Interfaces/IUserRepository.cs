namespace BE_AMPerfume.DAL.Interfaces;

using BE_AMPerfume.Core.Models;

public interface IUserRepository
{
    Task<User?> GetByEmailAndPasswordHashAsync(string email, string passwordHash);

    Task<User?> GetUserAsync(string email);             //admin
    Task<User?> GetByEmailAsync(string email);   //check exists
    Task<bool> UpdatePasswordAsync(string email, string passwordHash, string newPasswordHash); //update password
    Task<bool> UpdateAsync(User user);

    //Admin
    Task<IEnumerable<User>> GetAllAsync();
    

    Task CreateAsync(User user); //register
}
