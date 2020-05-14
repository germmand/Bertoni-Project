using AutoMapper;
using Bertoni.Core.Models;
using Bertoni.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bertoni.Web.Profiles {
    public class PhotoProfile : Profile {
        public PhotoProfile() {
            CreateMap<Photo, PhotosViewModel>();
        }
    }
}