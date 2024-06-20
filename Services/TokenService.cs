using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using Dagagino.Models;
using Microsoft.IdentityModel.Tokens;

namespace Dagagino.Services
{
    public enum DagaginoUserRole{
        CLIENT,
        SEllER,
        ADMIN
    }

    public class TokenService(JwtSecurityTokenHandler tokenHandler, JwtOptions options)
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = tokenHandler;
        private readonly JwtOptions _options = options;

        /* -------------------------------------------------------------------------- */
        /*                               Generate Token                               */
        /* -------------------------------------------------------------------------- */
        public string GenerateToken(string id, DagaginoUserRole role)
        {
            /* ------------------------- Token Decroptor ------------------------- */
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(30),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                /* --------------------------- Security Algorithm --------------------------- */
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey)),
                        SecurityAlgorithms.HmacSha256
                    ),
                /* ----------------------------- Subject Details ---------------------------- */
                Subject = new ClaimsIdentity(
                    [
                        new (ClaimTypes.NameIdentifier, id),
                        new (ClaimTypes.Role, role.ToString()),
                    ]
                ),
            };

            /* ----------------------------- Generate Token ----------------------------- */
            var securityToken = _tokenHandler.CreateToken(tokenDescriptior);

            /* ---------------------------- Convert To String --------------------------- */
            var accessToken = _tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }
}