using AutoMapper;
using CatsAPI.Models.DTO;
using CatsAPI.Models.Entities;

namespace CatsAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cat,CatDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name).ToList()));

            CreateMap<Tag, TagDto>().ReverseMap(); ;
        }
    }
}
