using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration) => _configuration = configuration;

        public string GenerateSha256Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();

            foreach (var @byte in bytes)
            {
                builder.Append(@byte.ToString("x2"));
            }

            return builder.ToString();
        }

        public string GenerateJwtToken(string email, Role role)
        {
            var key = _configuration["AuthSettings:Key"];
            var issuer = _configuration["AuthSettings:Issuer"];
            var audience = _configuration["AuthSettings:Audience"];
            var seconds = double.Parse(_configuration["AuthSettings:ExpirationSeconds"]);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new ("userName", email),
                new (ClaimTypes.Role, role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddSeconds(seconds),
                signingCredentials: credentials,
                claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
