using System;
using System.Threading.Tasks;
using Bertoni.Core.Models;
using System.Collections.Generic;

namespace Bertoni.Core.Services {
    public interface ITypicodeService {
        Task<ICollection<Album>> GetAlbumsAsync();
        Task<ICollection<Photo>> GetPhotosAsync(int albumId);
        Task<ICollection<Comment>> GetCommentsAsync(int photoId);
    }
}