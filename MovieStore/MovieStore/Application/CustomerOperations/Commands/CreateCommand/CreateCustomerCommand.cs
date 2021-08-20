
using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.Entities;

namespace MovieStore.Application.CustomerOperations.Commands.CreateCommand;

public class CreateCustomerCommand
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    public CreateCustomerModel Model { get; set; }

    public CreateCustomerCommand(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var customer = _context.Customers.FirstOrDefault(x=>x.Email==Model.Email); 
        if(customer is not null)
        {
            throw new InvalidOperationException("Bu Email Zaten Mevcut");
        }

        var result = _mapper.Map<Customer>(Model);

        _context.Customers.Add(result);
        _context.SaveChanges();

    }


}

public class CreateCustomerModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
