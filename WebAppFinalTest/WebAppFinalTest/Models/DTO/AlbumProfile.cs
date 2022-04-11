using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFinalTest.Models.DTO
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumDTO>()
            .ForMember(dest => dest.Band, opt => opt.MapFrom(src => src.Band.Name));
        }
    }
}
