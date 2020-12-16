using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.IdentityService.Application.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string email);
    }
}
