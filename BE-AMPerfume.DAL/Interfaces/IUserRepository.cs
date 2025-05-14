namespace BE_AMPerfume.DAL.Interfaces;
using BE_AMPerfume.Core.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();               // (không bắt buộc nếu chưa dùng)
    Task<User?> GetByEmailAsync(string email);           // ← kiểm tra tồn tại
    Task CreateAsync(User user);
}
