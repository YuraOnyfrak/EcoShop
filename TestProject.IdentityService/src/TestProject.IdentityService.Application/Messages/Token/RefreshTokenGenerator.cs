using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.IdentityService.Application.Messages.Token
{
    public class RefreshTokenGenerator : IRequest<string>
    {
    }
}
