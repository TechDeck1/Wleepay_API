using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services.Auth
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtTokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public string GenerateToken(UserDetail userInfo)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userInfo.FirstName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, userInfo.ApplicationUser.Email),
            new Claim("UserId", userInfo.UserId),
            new Claim(ClaimTypes.NameIdentifier, userInfo.UserId)
        };

            var appUser = _userManager.FindByIdAsync(userInfo.UserId).Result;
            if (appUser != null)
            {
                var roles = _userManager.GetRolesAsync(appUser).Result;
                var role = roles.FirstOrDefault();
                if (!string.IsNullOrEmpty(role))
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
