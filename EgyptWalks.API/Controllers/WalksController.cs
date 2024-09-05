using AutoMapper;
using EgyptWalks.API.CustomActionFilters;
using EgyptWalks.API.DTOs;
using EgyptWalks.Core;
using EgyptWalks.Core.Models.Domain;
using EgyptWalks.Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyptWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WalksController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Post: https:www.localhost:port/api/Regions
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<WalkDetailsDto>> Create([FromBody] WalkCreateDto walkCreateDto)
        {
            var walkDomainModel = _mapper.Map<Walk>(walkCreateDto);
            await _unitOfWork.Repository<Walk, Guid>().AddAsync(walkDomainModel);
            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<WalkDetailsDto>(walkDomainModel));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalkDetailsDto>>> GetAll([FromQuery] WalkSpecificationParameters parameters)
        {
            IBaseSpecification<Walk, Guid> spec = new WalkWthRegionAndDifficultySpec(parameters);
            //if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            //{
            //    if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            //        spec = new WalksByNameWithRegionAndDifficultySpec(filterQuery);
            //    else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
            //        spec = new WalksByDescriptionWithRegionAndDifficultySpec(filterQuery);
            //}

            var walkDomainModelList = await _unitOfWork.Repository<Walk, Guid>().GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<WalkDetailsDto>>(walkDomainModelList));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<WalkDetailsDto>> GetById([FromRoute] Guid id)
        {
            var spec = new WalkWthRegionAndDifficultySpec(id);
            var walkDomainModel = await _unitOfWork.Repository<Walk, Guid>().GetByIdWithSpecAsync(spec);
            if (walkDomainModel is null) return NotFound();
            return Ok(_mapper.Map<WalkDetailsDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<ActionResult<WalkDetailsDto>> Update([FromRoute] Guid id, [FromBody]WalkUpdateDto walkUpdateDto)
        {
            var spec = new WalkWthRegionAndDifficultySpec(id);
            var walkDomainModel = await _unitOfWork.Repository<Walk, Guid>().GetByIdWithSpecAsync(spec);
            if (walkDomainModel is null) return NotFound();

            walkDomainModel.Name = walkUpdateDto.Name;
            walkDomainModel.Description = walkUpdateDto.Description;
            walkDomainModel.LengthInKm = walkUpdateDto.LengthInKm;
            walkDomainModel.ImageUrl = walkUpdateDto.ImageUrl;
            walkDomainModel.DifficultyId = walkUpdateDto.DifficultyId;
            walkDomainModel.RegionId = walkUpdateDto.RegionId;
            _unitOfWork.Repository<Walk, Guid>().Update(walkDomainModel);
            await _unitOfWork.CompleteAsync();

            //Get the updated walk
            var updatedWalkFromDb = await _unitOfWork.Repository<Walk, Guid>().GetByIdWithSpecAsync(spec);

            return Ok(_mapper.Map<WalkDetailsDto>(updatedWalkFromDb));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await _unitOfWork.Repository<Walk, Guid>().GetByIdAsync(id);
            if (walkDomainModel is null) return NotFound();

            _unitOfWork.Repository<Walk, Guid>().Delete(walkDomainModel);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
