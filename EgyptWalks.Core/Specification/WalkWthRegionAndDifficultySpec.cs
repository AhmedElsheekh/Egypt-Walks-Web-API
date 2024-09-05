using EgyptWalks.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Specification
{
    public class WalkWthRegionAndDifficultySpec : BaseSpecification<Walk, Guid>
    {
        //Get All Walks
        public WalkWthRegionAndDifficultySpec(WalkSpecificationParameters parameters) : base(w => 
            (string.IsNullOrEmpty(parameters.Name) || w.Name.ToLower().Contains(parameters.Name))
            &&
            (string.IsNullOrEmpty(parameters.Description) || w.Description.ToLower().Contains(parameters.Description))
        )
        {
            Includes.Add(w => w.Difficulty);
            Includes.Add(w => w.Region);

            if(!string.IsNullOrEmpty(parameters.OrderBy))
            {
                switch(parameters.OrderBy)
                {
                    case "name":
                        AddOrderBy(w => w.Name);
                        break;
                    case "description":
                        AddOrderBy(w => w.Description);
                        break;
                    case "lengthinkm":
                        AddOrderBy(w => w.LengthInKm);
                        break;
                    default:
                        AddOrderBy(w => w.Name);
                        break;
                }

                if (parameters.IsAscendingOrder is not null)
                {
                    if (parameters.IsAscendingOrder == true)
                        IsAscendingOrder = true;
                    else
                        IsAscendingOrder = false;
                }
            }

            ApplyPagination(parameters.PageSize, (parameters.PageNumber - 1) * parameters.PageSize);
        }

        public WalkWthRegionAndDifficultySpec(Guid id) : base(w => w.Id == id)
        {
            Includes.Add(w => w.Difficulty);
            Includes.Add(w => w.Region);
        }

    }
}
