using AutoMapper;
using Common.Dtos.Post;
using Domain.Models;

namespace BL.Profiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostListDto>()
            .ForMember(x => x.Username, y => y.MapFrom(z => z.User.UserName))
            .ForMember(x => x.LikesCount, y => y.MapFrom(z => z.Likes.Count))
            .ForMember(x => x.Photo, y => y.MapFrom( z => z.Photos.FirstOrDefault()!.Id))
            .ForMember(x => x.PhotoExtension, y => y.MapFrom( z => z.Photos.FirstOrDefault()!.Extension));

        CreateMap<Post, PostDto>()
            .ForMember(x => x.Username, y => y.MapFrom(z => z.User.UserName));

        CreateMap<PostForUpdateDto, Post>();
    }
}