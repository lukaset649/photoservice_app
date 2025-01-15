//generuje token na bazie maila i identyfikatora użytkownika. Token ważny przez gofzinę
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using photoservice.Models;

namespace photoservice.Services
{
    public class JwtService
    {
        private readonly string _secretKey = "your_secret_key"; // Zmienna środowiskowa

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString())
            };

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "photoservice",
                audience: "photoservice",
                claims: claims,
                expires: DateTime.Now.AddHours(1), //token ważny przez godzinę
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
