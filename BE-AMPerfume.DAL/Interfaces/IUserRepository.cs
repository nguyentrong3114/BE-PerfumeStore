namespace BE_AMPerfume.DAL.Interfaces;

using BE_AMPerfume.Core.Models;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<User?> GetByEmailAndPasswordHashAsync(string email, string passwordHash);

    Task<User?> GetUserAsync(string email);             //admin
    Task<User?> GetByEmailAsync(string email);   //check exists
    Task<bool> UpdatePasswordAsync(string email, string oldPasswordHash, string newPasswordHash);
    Task<bool> UpdatePasswordByOtpAsync(string email, string newPasswordHash);
    Task<bool> UpdateAsync(User user);
    Task CreateAsync(User user); //register

    //Admin
    Task<IEnumerable<User>> GetAllAsync();


}
