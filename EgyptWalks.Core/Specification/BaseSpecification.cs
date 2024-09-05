using EgyptWalks.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Specification
{
    public class BaseSpecification<TModel, TKey> : IBaseSpecification<TModel, TKey> where TModel : BaseModel<TKey>
    {
        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<TModel, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TModel, bool>> Criteria { get ; set ; }
        public List<Expression<Func<TModel, object>>> Includes { get; set; } = new List<Expression<Func<TModel, object>>>();
        public Expression<Func<TModel, object>> OrderBy { get ; set ; }
        public bool IsAscendingOrder { get ; set ; }
        public int Take { get; set ; }
        public int Skip { get ; set ; }
        public bool IsPaginationApplied { get ; set; }

        public void ApplyPagination(int take, int skip)
        {
            IsPaginationApplied = true;
            Take = take;
            Skip = skip;
        }

        public void AddOrderBy(Expression<Func<TModel, object>> orderBy)
        {
            OrderBy = orderBy;
        }
    }
}
