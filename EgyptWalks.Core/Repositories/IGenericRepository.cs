using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Repositories
{
    public interface IGenericRepository<TModel, TKey> where TModel : BaseModel<TKey>
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel?> GetByIdAsync(TKey id);
        Task AddAsync(TModel model);
        void Update(TModel model);
        void Delete(TModel model);
        Task<IEnumerable<TModel>> GetAllWithSpecAsync(IBaseSpecification<TModel, TKey> spec);
        Task<TModel?> GetByIdWithSpecAsync(IBaseSpecification<TModel, TKey> spec);
    }
}
