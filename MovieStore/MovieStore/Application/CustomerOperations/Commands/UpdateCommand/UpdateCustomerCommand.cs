
using AutoMapper;
using MovieStore.DataAccess;

namespace MovieStore.Application.CustomerOperations.Commands.UpdateCommand;
public class UpdateCustomerCommand
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public UpdateCustomerModel Model { get; set; }
    public int CustomerId { get; set; }

    public UpdateCustomerCommand(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var customer = _context.Customers.FirstOrDefault(x=>x.Id==CustomerId);
        if (customer is not null)
        {
            throw new InvalidOperationException("Müşteri Bulunamadı");
        }
        customer.Name = Model.Name == default ? customer.Name : Model.Name;
        customer.Surname = Model.Surname == default ? customer.Surname : Model.Surname;
        customer.Password = Model.Password == default ? customer.Password : Model.Password;
        _context.SaveChanges();
    }
}

public class UpdateCustomerModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
}
