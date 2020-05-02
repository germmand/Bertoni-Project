using AutoMapper;
using Bertoni.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bertoni.Web.Profiles {
    public class AlbumProfile : Profile {
        public AlbumProfile() {
            CreateMap<Album, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Title));
        }
    }
}