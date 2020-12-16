using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.IdentityService.Domain.Entity
{
    public class User : IdentityUser<Guid>
    {
    }
}
