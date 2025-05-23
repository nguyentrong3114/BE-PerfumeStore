
using BE_AMPerfume.DAL.Data;

public class PaymentDetailRepostitory : IPaymentDetailRepository
{
    private readonly AMPerfumeDbContext _context;
    public PaymentDetailRepostitory(AMPerfumeDbContext context)
    {
        _context = context;
    }
    public Task Add(PaymentDetail paymentDetail)
    {
        _context.Add(paymentDetail);
        return Task.CompletedTask;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PaymentDetail> GetAll()
    {
        throw new NotImplementedException();
    }

    public PaymentDetail GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(PaymentDetail paymentDetail)
    {
        throw new NotImplementedException();
    }
}