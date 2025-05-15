using BE_AMPerfume.DAL.Data;
using BE_AMPerfume.DAL.Interfaces;
using System.Threading.Tasks;

namespace BE_AMPerfume.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AMPerfumeDbContext _context;

        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(
            AMPerfumeDbContext context,
            IProductRepository productRepository,
            IUserRepository userRepository)
         {
            _context = context;
            ProductRepository = productRepository;
            UserRepository = userRepository;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
