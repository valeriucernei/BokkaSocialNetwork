using AutoMapper;
using Common.Dtos.Post;
using Domain.Models;

namespace BL.Profiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostListDto>()
            .ForMember(x => x.User, y => y.MapFrom(z => z.User.FirstName + " " + z.User.LastName))
            .ForMember(x => x.LikesCount, y => y.MapFrom(z => z.Likes.Count));
    }
}