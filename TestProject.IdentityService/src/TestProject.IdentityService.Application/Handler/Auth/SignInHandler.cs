using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProject.IdentityService.Application.Messages.Command;
using TestProject.IdentityService.Application.Services;
using TestProject.IdentityService.Domain.Entity;

namespace TestProject.IdentityService.Application.Handler.Auth
{
    public class SignInHandler : IRequestHandler<SignInCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public SignInHandler
            (
            UserManager<User> userManager,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //if (!string.IsNullOrEmpty(request.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new Exception("Not found");//TODO: do normally exception

            if (await _userManager.CheckPasswordAsync(user, request.Password))
                return await _tokenService.GenerateToken(request.Email);

            throw new Exception("Password wrong");//TODO: do normally exception
        }
    }
}
