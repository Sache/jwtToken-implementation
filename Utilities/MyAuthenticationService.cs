using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace jwtTokens.Utilities
{
    public class MyAuthenticationService : IMyAuthenticationService
    {
        JwtTokenInfo _jwtTokenInfo = null;

        public MyAuthenticationService(IOptions<JwtTokenInfo> options)
        {
            _jwtTokenInfo = options.Value;
        }
        public bool isAuthenticated(UserPassword request, out string token)
        {
            token = string.Empty;
            if (request.username != request.password) return false;
            var claims = new[] { new Claim(ClaimTypes.Name, request.username) };
            var key  = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenInfo.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken
                (_jwtTokenInfo.Issuer, _jwtTokenInfo.Audience,
                claims, expires: DateTime.Now + TimeSpan.FromMinutes(Double.Parse(_jwtTokenInfo.AccessExpiry)),signingCredentials:credentials);
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;

        }
    }
}
