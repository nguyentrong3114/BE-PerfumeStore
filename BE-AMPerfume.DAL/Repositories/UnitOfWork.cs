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

        public ICartItemRepository CartItemsRepository{ get; }

        public ICartRepository CartRepository { get; }


        public UnitOfWork(
            AMPerfumeDbContext context,
            IProductRepository productRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemsRepository,
            IUserRepository userRepository)
         {
            _context = context;
            CartRepository = cartRepository;
            CartItemsRepository = cartItemsRepository;
            ProductRepository = productRepository;
            UserRepository = userRepository;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
