using EgyptWalks.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Specification
{
    public interface IBaseSpecification<TModel, TKey> where TModel : BaseModel<TKey>
    {
        Expression<Func<TModel, bool>> Criteria { get; set; }
        List<Expression<Func<TModel, object>>> Includes { get; set; }
        Expression<Func<TModel, object>> OrderBy { get; set; }
        bool IsAscendingOrder { get; set; }
        int Take { get; set; }
        int Skip { get; set; }
        bool IsPaginationApplied { get; set; }
    }
}
