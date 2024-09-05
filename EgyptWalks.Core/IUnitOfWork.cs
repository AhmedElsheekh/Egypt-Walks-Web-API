using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync();
        IGenericRepository<TModel, TKey> Repository<TModel, TKey>() where TModel : BaseModel<TKey>;
        IImageRepository ImageRepository { get; }
    }
}
