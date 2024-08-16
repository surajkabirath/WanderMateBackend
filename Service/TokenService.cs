
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;
using WanderMateBackend.Models;

namespace WanderMateBackend.Service
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            // provides the cryptographic tools needed for creating and validating tokens. It focuses on the security aspects like encryption, signing, and validation.`using Microsoft.IdentityModel.Tokens`

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                //Globally Unique Identifier
           new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username ?? string.Empty),
               new Claim(ClaimTypes.Role, user.Role)

            };
            // handling JSON Web Tokens (JWTs). It provides classes to create, serialize, and parse JWTs  using this package `using System.IdentityModel.Tokens.Jwt`
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;

        }
    }
}