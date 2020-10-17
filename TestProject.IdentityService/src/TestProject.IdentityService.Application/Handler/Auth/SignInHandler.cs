using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProject.IdentityService.Application.Messages.Command;
using TestProject.IdentityService.Domain.Entity;

namespace TestProject.IdentityService.Application.Handler.Auth
{
    public class SignInHandler : IRequestHandler<SignInCommand>
    {
        private readonly UserManager<User> _userManager;

        public SignInHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //if (!string.IsNullOrEmpty(request.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new Exception("Not found");//TODO: do normally exception

            if (await _userManager.CheckPasswordAsync(user, request.Password))
                return Unit.Value;

            throw new Exception("Password wrong");//TODO: do normally exception
        }
    }
}
