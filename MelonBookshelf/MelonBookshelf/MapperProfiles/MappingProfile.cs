using AutoMapper;
using MelonBookshelf.Models;

namespace MelonBookshelf.MapperProfiles
{
    public class MappingProfile  : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryViewModel,Category>().ReverseMap();
            CreateMap<FollowerViewModel, Follower>().ReverseMap();
            CreateMap<RequestViewModel, Request>().ReverseMap();
            CreateMap<RequestEditViewModel, Request>().ReverseMap();
            CreateMap<ResourceViewModel, Resource>().ReverseMap();
            CreateMap<ResourceEditViewModel, Resource>().ReverseMap();
            CreateMap<UpvoteViewModel, Upvote>().ReverseMap();
            CreateMap<WantedResourcesViewModel, WantedResources>().ReverseMap();
        }
    }
}
