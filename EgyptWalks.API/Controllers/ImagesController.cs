using EgyptWalks.API.DTOs;
using EgyptWalks.Core;
using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public ImagesController(IUnitOfWork unitOfWork)
        {
       
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto inputImage)
        {
            ValidateImageUpload(inputImage);

            if(ModelState.IsValid)
            {
                var imageModel = new Image()
                {
                    File = inputImage.File,
                    FileDescription = inputImage.Description,
                    FileExtension = Path.GetExtension(inputImage.File.FileName),
                    FileName = inputImage.FileName,
                    FileSizeInBytes = inputImage.File.Length,
                };

                imageModel = await _unitOfWork.ImageRepository.Upload(imageModel);
                return Ok(imageModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateImageUpload(ImageUploadDto inputImage)
        {
            var allowedExtensions = new List<string>() { ".png", ".jpg", ".jpeg" };

            if(!allowedExtensions.Contains(Path.GetExtension(inputImage.File.FileName)))
            {
                ModelState.AddModelError("File", "File extension is not allowed");
            }

            if(inputImage.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size is more than 10MB, Please upload smaller size");
            }
        }
    }
}
