using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Bertoni.Core.Services;
using Bertoni.Core.Models;
using Newtonsoft.Json;

namespace Bertoni.Web.Services {
    public class TypicodeService : ITypicodeService {

        private HttpClient Client { get; }

        public TypicodeService(HttpClient client) {
            client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/");
            Client = client;
        }

        public async Task<ICollection<Album>> GetAlbumsAsync() {
            var response = await Client.GetAsync("/albums");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            ICollection<Album> albums = JsonConvert.DeserializeObject<List<Album>>(jsonResponse);
            return albums;
        }

        public async Task<ICollection<Photo>> GetPhotosAsync(int albumId) {
            var response = await Client.GetAsync("/photos");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            ICollection<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(jsonResponse)
                .Where(photo => photo.AlbumId == albumId)
                .ToList();
            return photos;
        }

        public async Task<ICollection<Comment>> GetCommentsAsync(int photoId) {
            var response = await Client.GetAsync("/comments");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            ICollection<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(jsonResponse)
                .Where(comment => comment.PostId == photoId)
                .ToList();
            return comments;
        }

    }
}