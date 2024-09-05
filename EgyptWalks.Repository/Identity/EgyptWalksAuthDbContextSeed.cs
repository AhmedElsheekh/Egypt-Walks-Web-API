using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Repository.Identity
{
    public static class EgyptWalksAuthDbContextSeed
    {
        public static async Task SeedDataAsync(EgyptWalksAuthDbContext dbContext)
        {
            if(!dbContext.Roles.Any())
            {
                List<IdentityRole> rolesList = new List<IdentityRole>()
                {
                    new IdentityRole()
                    {
                        Name = "Reader",
                        NormalizedName = "Reader".ToUpper()
                    },
                    new IdentityRole()
                    {
                        Name = "Writer",
                        NormalizedName = "Writer".ToUpper()
                    }
                };

                await dbContext.Roles.AddRangeAsync(rolesList);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
