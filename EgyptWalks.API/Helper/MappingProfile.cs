using AutoMapper;
using EgyptWalks.API.DTOs;
using EgyptWalks.Core.Models.Domain;

namespace EgyptWalks.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionDetailsDto>();
            CreateMap<RegionCreateDto, Region>();
            CreateMap<WalkCreateDto, Walk>();
            CreateMap<Difficulty, DifficultyDetailsDto>();
            CreateMap<Walk, WalkDetailsDto>()
                .ForMember(d => d.DifficultyDetails, s => s.MapFrom(w => w.Difficulty))
                .ForMember(d => d.RegionDetails, s => s.MapFrom(w => w.Region));
            CreateMap<WalkUpdateDto, Walk>();
               
        }
    }
}
