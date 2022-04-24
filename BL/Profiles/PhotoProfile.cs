using AutoMapper;
using Common.Dtos.Photo;
using Domain.Models;

namespace BL.Profiles;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
        CreateMap<PhotoUploadDto, Photo>();
        CreateMap<Photo, PhotoDto>();
    }
}