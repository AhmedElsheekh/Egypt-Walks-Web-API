using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Repositories;
using EgyptWalks.Core.Specification;
using EgyptWalks.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Repository
{
    public class SQLGenericRepository<TModel, TKey> : IGenericRepository<TModel, TKey> where TModel : BaseModel<TKey>
    {
        private readonly EgyptWalksDbContext _dbContext;
        private readonly DbSet<TModel> _dbSet;

        public SQLGenericRepository(EgyptWalksDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TModel>();
        }

        public async Task AddAsync(TModel model)
            => await _dbSet.AddAsync(model);

        public void Delete(TModel model)
            => _dbSet.Remove(model);

        public async Task<IEnumerable<TModel>> GetAllAsync()
            => await _dbSet.ToListAsync();


        public async Task<TModel?> GetByIdAsync(TKey id)
            => await _dbSet.FindAsync(id);

        public void Update(TModel model)
            => _dbSet.Update(model);

        public async Task<IEnumerable<TModel>> GetAllWithSpecAsync(IBaseSpecification<TModel, TKey> spec)
            => await SpecificationEvaluator<TModel, TKey>.GetQuery(_dbSet, spec).ToListAsync();

        public async Task<TModel?> GetByIdWithSpecAsync(IBaseSpecification<TModel, TKey> spec)
            => await SpecificationEvaluator<TModel, TKey>.GetQuery(_dbSet, spec).FirstOrDefaultAsync();

    }
}
