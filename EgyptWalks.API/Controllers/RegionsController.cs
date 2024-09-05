using AutoMapper;
using EgyptWalks.API.CustomActionFilters;
using EgyptWalks.API.DTOs;
using EgyptWalks.Core;
using EgyptWalks.Core.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<ActionResult<IEnumerable<RegionDetailsDto>>> GetAll()
        {
            var regionsFromDb = await _unitOfWork.Repository<Region, Guid>().GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RegionDetailsDto>>(regionsFromDb));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<ActionResult<RegionDetailsDto>> GetById([FromRoute]Guid id)
        {          
            var regionFromDb = await _unitOfWork.Repository<Region, Guid>().GetByIdAsync(id);
            if (regionFromDb is null) return NotFound();
            return Ok(_mapper.Map<RegionDetailsDto>(regionFromDb));

        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<ActionResult<RegionDetailsDto>> Create([FromBody]RegionCreateDto inputRegion)
        {

            var regionModel = _mapper.Map<Region>(inputRegion);
            await _unitOfWork.Repository<Region, Guid>().AddAsync(regionModel);
            await _unitOfWork.CompleteAsync();

            var regionDetailsDto = _mapper.Map<RegionDetailsDto>(regionModel);
            return CreatedAtAction(nameof(GetById), new { id = regionModel.Id }, regionDetailsDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<ActionResult<RegionDetailsDto>> Update([FromRoute] Guid id, [FromBody]RegionUpdateDto inputRegion)
        {

            var regionFromDb = await _unitOfWork.Repository<Region, Guid>().GetByIdAsync(id);
            if (regionFromDb is null) return NotFound();

            regionFromDb.Name = inputRegion.Name;
            regionFromDb.ImageUrl = inputRegion.ImageUrl;

            _unitOfWork.Repository<Region, Guid>().Update(regionFromDb);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<RegionDetailsDto>(regionFromDb));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var regionFromDb = await _unitOfWork.Repository<Region, Guid>().GetByIdAsync(id);
            if (regionFromDb is null) return NotFound();

            _unitOfWork.Repository<Region, Guid>().Delete(regionFromDb);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}
