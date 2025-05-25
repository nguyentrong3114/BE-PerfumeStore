using BE_AMPerfume.DAL.Data;
using BE_AMPerfume.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace BE_AMPerfume.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AMPerfumeDbContext _context;

        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }

        public ICartItemRepository CartItemsRepository { get; }

        public ICartRepository CartRepository { get; }

        public IPaymentRepository PaymentRepository { get; }
        public IAnalyticsRepository AnalyticsRepository{ get; }
        public IPaymentDetailRepository PaymentDetailRepostitory { get; }
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }
        public UnitOfWork(
            AMPerfumeDbContext context,
            IAnalyticsRepository analyticsRepository,
            IProductRepository productRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemsRepository,
            IUserRepository userRepository,
            IPaymentRepository paymentRepository,
            IPaymentDetailRepository paymentDetailRepository)
        {
            _context = context;
            AnalyticsRepository = analyticsRepository;
            CartRepository = cartRepository;
            CartItemsRepository = cartItemsRepository;
            ProductRepository = productRepository;
            UserRepository = userRepository;
            PaymentRepository = paymentRepository;
            PaymentDetailRepostitory = paymentDetailRepository;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
