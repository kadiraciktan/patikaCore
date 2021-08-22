using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.Application.OrderOperations.Commands.DeleteOrder;
using MovieStore.Application.OrderOperations.Commands.UpdateOrder;
using MovieStore.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStore.Application.OrderOperations.Queries.GetOrders;
using MovieStore.DataAccess;

namespace MovieStore.Controllers;
[Authorize]
[Route("api/[controller]s")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;

    public OrderController(IMovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public List<OrdersViewModel> GetOrders()
    {
        GetOrdersQuery query = new GetOrdersQuery(_context, _mapper);
        return query.Handle();

    }

    [HttpGet("{id}")]
    public OrderDetailViewModel GetOrderDetail(int id)
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = id;
        GetOrderDetailQueryValidator validationRules = new GetOrderDetailQueryValidator();
        validationRules.ValidateAndThrow(query);
        return query.Handle();
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody]CreateOrderModel model)
    {
        CreateOrderCommand command = new CreateOrderCommand(_context,_mapper);
        command.Model = model;
        CreateOrderCommandValidator validationRules = new CreateOrderCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrder([FromBody] UpdateOrderModel model,int id)
    {
        UpdateOrderCommand command = new UpdateOrderCommand(_context,_mapper);
        command.Model= model;
        command.OrderId = id;
        UpdateOrderCommandValidator validationRules = new UpdateOrderCommandValidator();
        validationRules.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.OrderId = id;
        DeleteOrderCommandValidator validationRules = new DeleteOrderCommandValidator();
        validationRules.ValidateAndThrow(command); 
        command.Handle();
        return Ok();
    }


}
