
using MovieStore.DataAccess;

namespace MovieStore.Application.OrderOperations.Commands.DeleteOrder;
public class DeleteOrderCommand
{
    private readonly IMovieContext _context;
    public int OrderId { get; set; }
    public DeleteOrderCommand(IMovieContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var order = _context.Orders.FirstOrDefault(x => x.Id == OrderId);
        if (order is null)
        {
            throw new InvalidOperationException("Sipariş Bulunamadı");
        }
        order.IsActive = false;
        _context.SaveChanges();
    }
}
