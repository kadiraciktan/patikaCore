using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.CustomerOperations.Commands;
using MovieStore.Application.CustomerOperations.Commands.CreateCommand;
using MovieStore.Application.CustomerOperations.Commands.DeleteCommand;
using MovieStore.Application.CustomerOperations.Commands.RefreshToken;
using MovieStore.Application.CustomerOperations.Commands.UpdateCommand;
using MovieStore.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.Application.CustomerOperations.Queries.GetCustomers;
using MovieStore.Application.CustomerOperations.Queries.LoginCustomer;
using MovieStore.DataAccess;
using MovieStore.TokenOperations.Models;
using static MovieStore.Application.CustomerOperations.Queries.GetCustomerDetail.GetCustomerDetailQuery;
using static MovieStore.Application.CustomerOperations.Queries.GetCustomers.GetCustomersQuery;

namespace MovieStore.Controllers;
[Route("api/[controller]s")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CustomerController(IMovieContext context, IMapper mapper,IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }



    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] LoginCustomerModel login)
    {
        LoginCustomerQuery command = new LoginCustomerQuery(_context, _mapper, _configuration);
        command.Model = login;
        LoginCustomerQueryValidator validationRules = new LoginCustomerQueryValidator();
        validationRules.ValidateAndThrow(command);
        var token = command.Handle();
        return Ok(token);
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
        command.RefreshToken = token;
        var resultToken = command.Handle();
        return Ok(resultToken);
    }

   [Authorize]
    [HttpGet]
    public List<CustomersViewModel> GetCustomers()
    {
        GetCustomersQuery query = new GetCustomersQuery(_context,_mapper);
        return query.Handle();

    }

   [Authorize]
    [HttpGet("{id}")]
    public CustomerDetailViewModel GetCustomerDetail(int id)
    {
        GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context,_mapper);
        query.CustomerId = id;
        GetCustomerDetailQueryValidator validationRules = new GetCustomerDetailQueryValidator();
        validationRules.ValidateAndThrow(query);
        return query.Handle();
    }

    [Authorize]
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

     [Authorize]
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

     [Authorize]
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
