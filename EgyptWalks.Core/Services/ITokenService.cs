using EgyptWalks.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Services
{
    public interface ITokenService
    {
        string CreateJWTToken(ApplicationUser applicationUser, List<string> roles);
    }
}
