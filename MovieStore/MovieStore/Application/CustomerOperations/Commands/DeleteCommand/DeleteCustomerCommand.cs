
using MovieStore.DataAccess;

namespace MovieStore.Application.CustomerOperations.Commands.DeleteCommand;
public class DeleteCustomerCommand
{
    private readonly IMovieContext _context;
    public int CustomerId { get; set; }

    public DeleteCustomerCommand(IMovieContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var customer  = _context.Customers.FirstOrDefault(x=> x.Id == CustomerId);
        if (customer is null)
        {
            throw new InvalidOperationException("Müşteri Bulunamadı");
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

    }

}
