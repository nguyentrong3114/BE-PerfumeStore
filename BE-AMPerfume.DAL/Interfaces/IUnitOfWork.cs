using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace BE_AMPerfume.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        ICartItemRepository CartItemsRepository { get; }
        ICartRepository CartRepository { get; }
        IPaymentDetailRepository PaymentDetailRepostitory { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}
