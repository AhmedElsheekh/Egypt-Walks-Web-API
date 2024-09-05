using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Repository
{
    public static class SpecificationEvaluator<TModel, TKey> where TModel : BaseModel<TKey>
    {
        public static IQueryable<TModel> GetQuery(IQueryable<TModel> inputQuery, IBaseSpecification<TModel, TKey> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
            {
                if (spec.IsAscendingOrder)
                    query = query.OrderBy(spec.OrderBy);
                else
                    query = query.OrderByDescending(spec.OrderBy);
            }

            query = spec.Includes.Aggregate(query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            if (spec.IsPaginationApplied)
                query = query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
