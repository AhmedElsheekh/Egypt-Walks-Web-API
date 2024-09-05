using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Specification
{
    public class WalkSpecificationParameters
    {
        private string? name;
        public string? Name
        {
            get { return name; }
            set { name = value is not null ? value.ToLower() : null; }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set { description = value is not null ? value.ToLower() : null; }
        }

        private string? orderBy;
        public string? OrderBy
        {
            get { return orderBy; }
            set { orderBy = value is not null ? value.ToLower() : null; }
        }

        public bool? IsAscendingOrder { get; set; }

        public int PageNumber { get; set; } = 1;

        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
    }
}
