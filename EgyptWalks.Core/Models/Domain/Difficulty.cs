using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Models.Domain
{
    public class Difficulty : BaseModel<Guid>
    {
        public string Name { get; set; }
    }
}
