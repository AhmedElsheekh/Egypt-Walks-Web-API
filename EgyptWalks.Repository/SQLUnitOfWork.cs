using EgyptWalks.Core;
using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Repositories;
using EgyptWalks.Repository.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Repository
{
    public class SQLUnitOfWork : IUnitOfWork
    {
        private readonly EgyptWalksDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private Hashtable _repositories;
        private IImageRepository _imageRepository;

        public SQLUnitOfWork(EgyptWalksDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _repositories = new Hashtable();
        }

        public IImageRepository ImageRepository
        {
            get
            {
                return _imageRepository ??= new LocalImageRepository(
                    Path.Combine(_webHostEnvironment.WebRootPath, "Files", "Images"),
                    _httpContextAccessor,
                    _dbContext
                    );
            }
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();

        public IGenericRepository<TModel, TKey> Repository<TModel, TKey>() where TModel : BaseModel<TKey>
        {
            var repoName = typeof(TModel).Name;
            if(!_repositories.ContainsKey(repoName))
            {
                var repoValue = new SQLGenericRepository<TModel, TKey>(_dbContext);
                _repositories.Add(repoName, repoValue);
            }
            return _repositories[repoName] as IGenericRepository<TModel, TKey>;
        }
    }
}
