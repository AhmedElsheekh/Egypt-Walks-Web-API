using EgyptWalks.Core.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Repository.Identity
{
    public class EgyptWalksAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public EgyptWalksAuthDbContext(DbContextOptions<EgyptWalksAuthDbContext> options) : base(options)
        {
        }
    }
}
