using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProject.IdentityService.Application.Common.Interfaces;
using TestProject.IdentityService.Application.Messages.Command;
using TestProject.IdentityService.Domain.Entity;

namespace TestProject.IdentityService.Application.Handler.Auth
{
    public class SignUpHandler : IRequestHandler<SignUpCommand>
    {
        private readonly UserManager<User> _userManager;

        public SignUpHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
                throw new Exception("user exist with the same e-mail");//TODO: do normally exception

            user = new User
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if(result.Succeeded)
                return Unit.Value;

            throw new Exception(result.Errors.ToString());//TODO: do normally exception
        }
    }
}
