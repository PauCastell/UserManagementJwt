using Api.Configuration;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure.Services
{
    public class JwtTokenGeneratorService : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGeneratorService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: CreateClaims(user),
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: SignCredentials()
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> CreateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Sub: subject id usuario
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Jti: identificador único del jwt
            };
        }

        private SymmetricSecurityKey CreateSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)); //Transforma la clave en bytes, para la firma del jwet
        }

        private SigningCredentials SignCredentials()
        {
            return new SigningCredentials(CreateSecurityKey(), SecurityAlgorithms.HmacSha256);
        }
    }
}
