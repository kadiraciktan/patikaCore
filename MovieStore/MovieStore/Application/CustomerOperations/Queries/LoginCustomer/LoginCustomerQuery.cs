
using AutoMapper;
using MovieStore.DataAccess;
using MovieStore.TokenOperations;
using MovieStore.TokenOperations.Models;

namespace MovieStore.Application.CustomerOperations.Queries.LoginCustomer;
public class LoginCustomerQuery
{
    private readonly IMovieContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public LoginCustomerModel Model { get; set; }

    public LoginCustomerQuery(IMovieContext context, IMapper mapper,IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var customer = _context.Customers.FirstOrDefault(x=>x.Email==Model.Email && x.Password==Model.Password);
        if (customer is null)
        {
            throw new InvalidOperationException("Kullanıcı Adı veya Şifre Hatalı");
        }
        TokenHandler tokenHandler = new TokenHandler(_configuration);
        Token token = tokenHandler.CreateAccessToken();
        customer.RefreshToken = token.RefreshToken;
        customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();
        return token;
    }


}

public class LoginCustomerModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
