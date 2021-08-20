using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.CustomerOperations.Commands;
using MovieStore.Application.CustomerOperations.Commands.CreateCommand;
using MovieStore.Application.CustomerOperations.Commands.DeleteCommand;
using MovieStore.Application.CustomerOperations.Commands.UpdateCommand;
using MovieStore.Application.CustomerOperations.Queries.GetCustomers;
using MovieStore.DataAccess;
using static MovieStore.Application.CustomerOperations.Queries.GetCustomers.GetCustomersQuery;

namespace MovieStore.Controllers;
[Route("api/[controller]s")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public CustomerController(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpGet]

    public List<CustomersViewModel> GetCustomers()
    {
        GetCustomersQuery query = new GetCustomersQuery(_context,_mapper);
        return query.Handle();

    }

    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CreateCustomerModel model)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_context,_mapper);
        command.Model = model;
        CreateCustomerCommandValidator validationRules = new CreateCustomerCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomer([FromBody]UpdateCustomerModel model,int id)
    {
        UpdateCustomerCommand command = new UpdateCustomerCommand(_context,_mapper);
        command.CustomerId = id;
        command.Model = model;
        UpdateCustomerCommandValidator validationRules = new UpdateCustomerCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        DeleteCustomerCommand command =  new DeleteCustomerCommand(_context);
        command.CustomerId = id;
        DeleteCustomerCommandValidator validationRules = new DeleteCustomerCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    


}
