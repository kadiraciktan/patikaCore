


using MovieStore.DataAccess;
using MovieStore.TokenOperations;
using MovieStore.TokenOperations.Models;

namespace MovieStore.Application.CustomerOperations.Commands.RefreshToken;
public class RefreshTokenCommand
{

    public string RefreshToken { get; set; }

    private readonly IMovieContext _context;
    private readonly IConfiguration _configuration;
    public RefreshTokenCommand(IMovieContext context, IConfiguration configuration)
    {
        _context = context;

        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Customers.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
        if (user is not null)
        {
            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken();
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            _context.SaveChanges();
            return token;
        }
        else
        {
            throw new InvalidOperationException("Valid Bir Refresh Token Bulunamadı");
        }
    }

    public class CreateTokenModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
