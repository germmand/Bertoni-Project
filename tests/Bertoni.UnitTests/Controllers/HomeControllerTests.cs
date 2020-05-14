using Bertoni.Web.Controllers;
using Bertoni.Core.Services;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using Bertoni.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Bertoni.Web.Models;
using FluentAssertions;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bertoni.UnitTests.Controllers {
    public class HomeControllerTests {
        public readonly HomeController _homeController;
        public readonly Mock<ITypicodeService> _typicodeServiceMock;
        public readonly Mock<IMapper> _mapperMock;
        public readonly Mock<ILogger<HomeController>> _loggerMock;

        public HomeControllerTests()
        {
            _typicodeServiceMock = new Mock<ITypicodeService>();
            _mapperMock          = new Mock<IMapper>();
            _loggerMock          = new Mock<ILogger<HomeController>>();
            _homeController = new HomeController(
                _loggerMock.Object, 
                _typicodeServiceMock.Object, 
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithListOfAlbums() {
            var albums = new List<Album>() {
                new Album() {
                    UserId = 1,
                    Id = 1,
                    Title = "quidem molestiae enim"
                }, 
                new Album() {
                    UserId = 1,
                    Id = 2,
                    Title = "sunt qui excepturi placeat culpa"
                }
            };
            var expectedViewModel = new AlbumsViewModel() {
                AlbumId = albums.ElementAt(0).Id,
                Albums = new List<SelectListItem>() {
                    new SelectListItem() {
                        Text = albums.ElementAt(0).Title,
                        Value = albums.ElementAt(0).Id.ToString()
                    },
                    new SelectListItem() {
                        Text = albums.ElementAt(1).Title,
                        Value = albums.ElementAt(1).Id.ToString()
                    }
                }
            };
            _typicodeServiceMock
                .Setup<Task<ICollection<Album>>>(service => service.GetAlbumsAsync())
                .ReturnsAsync(albums);
            _mapperMock
                .Setup(mapper => mapper.Map<List<SelectListItem>>(It.IsAny<ICollection<Album>>()))
                .Returns(expectedViewModel.Albums);
            var result = await _homeController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AlbumsViewModel>(viewResult.ViewData.Model);
            model.Should().BeEquivalentTo(expectedViewModel);
        }

        [Fact]
        public void Index_ReturnsViewResult_WhenModelStateIsInvalid() {
            var albumId = 1;
            var albumsViewModel = new AlbumsViewModel() {
                AlbumId = albumId,
                Albums = new List<SelectListItem>() {
                    new SelectListItem() {
                        Text = "quidem molestiae enim",
                        Value = albumId.ToString(),
                    },
                    new SelectListItem() {
                        Text = "sunt qui excepturi placeat culpa",
                        Value = "2"
                    }
                }
            };
            _homeController.ModelState.AddModelError("Albums", "Required");
            var result = _homeController.Index(albumsViewModel);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AlbumsViewModel>(viewResult.ViewData.Model);
            model.Should().BeEquivalentTo(albumsViewModel);
        }

        [Fact]
        public void Index_RedirectsToPhotos_WhenModelStateIsValid() {
            var albumId = 1;
            var albumsViewModel = new AlbumsViewModel() {
                AlbumId = albumId,
                Albums = new List<SelectListItem>() {
                    new SelectListItem() {
                        Text = "quidem molestiae enim",
                        Value = albumId.ToString(),
                    },
                    new SelectListItem() {
                        Text = "sunt qui excepturi placeat culpa",
                        Value = "2"
                    }
                }
            };
            var expectedAction = "Photos";
            var result = _homeController.Index(albumsViewModel);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(expectedAction, redirectToActionResult.ActionName);
            Assert.Equal(redirectToActionResult.RouteValues["albumId"], albumId);
        }

        [Fact]
        public async Task Photos_ReturnsViewResult_WithListOfPhotosGivenAlbumId() {
            var albumId = 1;
            var photos = new List<Photo>() {
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
                }
            };
            var photosViewModel = new List<PhotosViewModel>() {
                new PhotosViewModel() {
                    AlbumId = photos.ElementAt(0).AlbumId,
                    Id = photos.ElementAt(0).Id,
                    Title = photos.ElementAt(0).Title,
                    Url = photos.ElementAt(0).Url,
                    ThumbnailUrl = photos.ElementAt(0).ThumbnailUrl
                },
                new PhotosViewModel() {
                    AlbumId = photos.ElementAt(1).AlbumId,
                    Id = photos.ElementAt(1).Id,
                    Title = photos.ElementAt(1).Title,
                    Url = photos.ElementAt(1).Url,
                    ThumbnailUrl = photos.ElementAt(1).ThumbnailUrl
                },
            };
            _mapperMock
                .Setup(mapper => mapper.Map<List<PhotosViewModel>>(It.IsAny<List<Photo>>()))
                .Returns(photosViewModel);
            _typicodeServiceMock
                .Setup<Task<ICollection<Photo>>>(service => service.GetPhotosAsync(albumId))
                .ReturnsAsync(photos);
            var result = await _homeController.Photos(albumId);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PhotosViewModel>>(viewResult.ViewData.Model);
            model.Should().BeEquivalentTo(photosViewModel);
        }

        [Fact]
        public async Task Comments_ReturnsListOfComments_GivenPhotoId() {
            var photoId = 1;
            var comments = new List<Comment>() {
                new Comment() {
                    PostId = photoId,
                    Id = 1,
                    Name = "id labore ex et quam laborum",
                    Email = "Eliseo@gardner.biz",
                    Body =  "laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora " +
                            "quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium"
                },
                new Comment() {
                    PostId = photoId,
                    Id = 2,
                    Name = "quo vero reiciendis velit similique earum",
                    Email = "Jayne_Kuhic@sydney.com",
                    Body = "est natus enim nihil est dolore omnis voluptatem numquam\net omnis" +
                           "occaecati quod ullam at\nvoluptatem error expedita pariatur\nnihil sint nostrum voluptatem reiciendis et"
                }
            };
            var commentsViewModel = new List<CommentViewModel>() {
                new CommentViewModel() {
                    PostId = comments.ElementAt(0).PostId,
                    Id = comments.ElementAt(0).Id,
                    Name = comments.ElementAt(0).Name,
                    Email = comments.ElementAt(0).Email,
                    Body = comments.ElementAt(0).Body
                },
                new CommentViewModel() {
                    PostId = comments.ElementAt(1).PostId,
                    Id = comments.ElementAt(1).Id,
                    Name = comments.ElementAt(1).Name,
                    Email = comments.ElementAt(1).Email,
                    Body = comments.ElementAt(1).Body
                }
            };
            _mapperMock
                .Setup<List<CommentViewModel>>(mapper => mapper.Map<List<CommentViewModel>>(It.IsAny<List<Comment>>()))
                .Returns(commentsViewModel);
            _typicodeServiceMock
                .Setup<Task<ICollection<Comment>>>(service => service.GetCommentsAsync(photoId))
                .ReturnsAsync(comments);
            var result = await _homeController.Comments(photoId);
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<List<CommentViewModel>>(viewResult.ViewData.Model);
            model.Should().BeEquivalentTo(commentsViewModel);
        }
    }
}
