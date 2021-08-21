
using Microsoft.IdentityModel.Tokens;
using MovieStore.TokenOperations.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MovieStore.TokenOperations;
public class TokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public Token CreateAccessToken()
    {

        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        tokenModel.Expiration = DateTime.Now.AddMinutes(15);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            expires: tokenModel.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        //JWT CLAİM EKLENMELİ.

        tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }

    private string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString().Replace("-","");
    }
}
