using AutoMapper;
using MelonBookshelf.Models;

namespace MelonBookshelf.MapperProfiles
{
    public class MappingProfile  : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryViewModel,Category>().ReverseMap();
        }
    }
}
