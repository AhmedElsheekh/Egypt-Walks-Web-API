using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Repositories;
using EgyptWalks.Repository.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Repository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly string _imagePath;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EgyptWalksDbContext _dbContext;

        public LocalImageRepository(string imagePath,
            IHttpContextAccessor httpContextAccessor,
            EgyptWalksDbContext dbContext)
        {
            _imagePath = imagePath;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            //Get Local Image Path
            var localImagePath = Path.Combine(_imagePath, $"{image.FileName}{image.FileExtension}");

            //Copy the image to local image path
            using var stream = new FileStream(localImagePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //Get the url for image
            var imageUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/wwwroot/Files/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = imageUrl;

            //Save To Database
            await _dbContext.Images.AddAsync(image);
            //await _dbContext.SaveChangesAsync();
            return image;
        }
    }
}
