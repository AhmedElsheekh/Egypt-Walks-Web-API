using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Models.Domain
{
    public class Region : BaseModel<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ImageUrl { get; set; }
    }
}
