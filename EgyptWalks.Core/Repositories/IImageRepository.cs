using EgyptWalks.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
