using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentalCar.Data.Repositories;
using RentalCar.Model.Models;

namespace RentalCar.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repository;

        public TokenService(IConfiguration configuration, IUserRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Fullname),
            };

            // var claims = new List<Claim>();
            // claims.Add(new Claim("username", user.Username));
            // claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Username));

            // Add roles as multiple claims
            var roles = _repository.GetRolesOfUser(user.Id);

            // _repository.GetRolesOfUser(user.Username);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var symmetricKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    symmetricKey, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
 
            var token = tokenHandler.CreateToken(tokenDescriptor);
 
            return tokenHandler.WriteToken(token);
        }
    }
}