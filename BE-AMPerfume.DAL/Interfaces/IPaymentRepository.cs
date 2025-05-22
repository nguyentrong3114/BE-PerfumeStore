using System.Threading.Tasks;
using BE_AMPerfume.Core.Models;

namespace BE_AMPerfume.DAL.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByCartIdAsync(int cartId);
        Task<Payment?> GetByTransactionCodeAsync(string transactionCode);
        Task<Payment?> ShowTransaction(int cartId);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(int id);
    }
}
