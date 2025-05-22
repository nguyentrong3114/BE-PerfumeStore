using System.Threading.Tasks;

namespace BE_AMPerfume.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        ICartItemRepository CartItemsRepository { get; }
        ICartRepository CartRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
