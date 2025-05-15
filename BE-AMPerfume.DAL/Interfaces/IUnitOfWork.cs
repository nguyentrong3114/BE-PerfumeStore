using System.Threading.Tasks;

namespace BE_AMPerfume.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
