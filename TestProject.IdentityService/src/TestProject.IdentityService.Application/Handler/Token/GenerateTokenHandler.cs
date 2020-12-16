using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Common.Authentication;
using TestProject.IdentityService.Application.Messages.Token;
using TestProject.IdentityService.Domain.Entity;

namespace TestProject.IdentityService.Application.Handler.Token
{
    public class GenerateTokenHandler : IRequestHandler<TokenGenerator, string>
    {
        private readonly JwtOptions _options;
        private readonly UserManager<User> _userManager;
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public GenerateTokenHandler(IConfiguration configuration, UserManager<User> userManager)
        {
            _userManager = userManager;        
            _options = configuration.GetSection("jwt").Get<JwtOptions>();

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.ValidAudience,
                ValidateAudience = _options.ValidateAudience,
                ValidateLifetime = _options.ValidateLifetime
            };
        }

        public async Task<string> Handle(TokenGenerator request, CancellationToken cancellationToken)
        {
            request.Email = "test5@ukr.net";
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new Exception("Not found");//TODO: do normally exception

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, now.ToString()),
            };

            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: jwtClaims,
                //notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );            

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
