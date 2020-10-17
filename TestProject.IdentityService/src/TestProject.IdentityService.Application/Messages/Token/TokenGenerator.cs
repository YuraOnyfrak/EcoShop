using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.IdentityService.Application.Messages.Token
{
    public class TokenGenerator : IRequest<string>
    {
        public string Email { get; set; }
    }
}
