using Moq;
using System.Collections.Generic;
using Bertoni.Core.Models;
using Moq.Protected;
using Xunit;
using System.Net.Http;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Bertoni.Core.Services;
using Bertoni.Web.Services;
using Newtonsoft.Json;
using FluentAssertions;

namespace Bertoni.Tests.Services {
    public class TypicodeServiceTests 
    {
        public readonly ITypicodeService _typicodeService;
        public readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        public readonly HttpClient _httpClient;

        public TypicodeServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _typicodeService = new TypicodeService(_httpClient);             
        }

        [Fact]
        public async Task GetAlbumsAsync_CallsToAlbums_ReturnsMappedListOfAlbums() 
        {
            var expectedAlbums = new List<Album>() {
                new Album() {
                    UserId = 1,
                    Id = 1,
                    Title = "quidem molestiae enim"
                }, 
                new Album() {
                    UserId = 1,
                    Id = 2,
                    Title = "sunt qui excepturi placeat culpa"
                },
                new Album() {
                    UserId = 1,
                    Id = 3,
                    Title = "omnis laborum odio"
                }
            };
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedAlbums, Formatting.Indented))
                })
                .Verifiable();
                var result = await _typicodeService.GetAlbumsAsync();
                result.Should().BeEquivalentTo(expectedAlbums);
        }

        [Fact]
        public async Task GetPhotosAsync_CallsToPhotos_ReturnsMappedListOfPhotosGivenUserId() 
        {
            var albumId = 1;
            var allPhotos = new List<Photo>() {
                new Photo() {
                    AlbumId = albumId,
                    Id = 1,
                    Title = "accusamus beatae ad facilis cum similique qui sunt",
                    Url = "https://via.placeholder.com/600/92c952",
                    ThumbnailUrl = "https://via.placeholder.com/150/92c952"
                },
                new Photo() {
                    AlbumId = albumId,
                    Id = 2,
                    Title = "reprehenderit est deserunt velit ipsam",
                    Url = "https://via.placeholder.com/600/771796",
                    ThumbnailUrl = "https://via.placeholder.com/150/771796"
                },
                new Photo() {
                    AlbumId = albumId + 1,
                    Id = 3,
                    Title = "officia porro iure quia iusto qui ipsa ut modi",
                    Url = "https://via.placeholder.com/600/24f355",
                    ThumbnailUrl = "https://via.placeholder.com/150/24f355"
                },
            };
            var expectedPhotos = allPhotos.Where(photo => photo.AlbumId == albumId).ToList();
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(allPhotos, Formatting.Indented))
                })
                .Verifiable();
            var result = await _typicodeService.GetPhotosAsync(albumId);
            result.Should().BeEquivalentTo(expectedPhotos);
        }

        [Fact]
        public async Task GetCommentsAsync_CallsToComments_ReturnsMappedListOfCommentsGivenPhotoId() 
        {
            var photoId = 1;
            var allComments = new List<Comment>() {
                new Comment() {
                    PostId = photoId,
                    Id = 1,
                    Name = "id labore ex et quam laborum",
                    Email = "Eliseo@gardner.biz",
                    Body = "laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo " +
                           "necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium"
                },
                new Comment() {
                    PostId = photoId,
                    Id = 2,
                    Name = "quo vero reiciendis velit similique earum",
                    Email = "Jayne_Kuhic@sydney.com",
                    Body = "est natus enim nihil est dolore omnis voluptatem numquam\net omnis occaecati " + 
                           "quod ullam at\nvoluptatem error expedita pariatur\nnihil sint nostrum voluptatem reiciendis et"
                },
                new Comment() {
                    PostId = photoId + 1,
                    Id = 3,
                    Name = "odio adipisci rerum aut animi",
                    Email = "Nikita@garfield.biz",
                    Body = "quia molestiae reprehenderit quasi aspernatur\naut expedita occaecati " + 
                           "aliquam eveniet laudantium\nomnis quibusdam delectus saepe quia accusamus " +
                           "maiores nam est\ncum et ducimus et vero voluptates excepturi deleniti ratione"
                }
            };
            var expectedComments = allComments.Where(comment => comment.PostId == photoId).ToList();
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(allComments, Formatting.Indented))
                })
                .Verifiable();
            var result = await _typicodeService.GetCommentsAsync(photoId);
            result.Should().BeEquivalentTo(expectedComments);
        }
    }
}