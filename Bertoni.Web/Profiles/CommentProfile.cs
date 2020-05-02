using AutoMapper;
using Bertoni.Core.Models;
using Bertoni.Web.Models;

namespace Bertoni.Web.Profiles {
    public class CommentProfile : Profile {
        public CommentProfile() {
            CreateMap<Comment, CommentViewModel>();
        }
    }
}