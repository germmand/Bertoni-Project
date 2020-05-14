using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bertoni.Web.Models;
using Bertoni.Core.Services;
using AutoMapper;

namespace Bertoni.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITypicodeService _typicodeService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
                              ITypicodeService typicodeService,
                              IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _typicodeService = typicodeService;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _typicodeService.GetAlbumsAsync();
            var albumsViewModel = new AlbumsViewModel() 
            {
                AlbumId = albums.ElementAt(0).Id,
                Albums = _mapper.Map<List<SelectListItem>>(albums),
            };
            return View(albumsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AlbumsViewModel model) {
            if (ModelState.IsValid) {
                return RedirectToAction("Photos", new { albumId = model.AlbumId });
            }
            return View(model);
        }

        [Route("Album/{albumId}/Photos")]
        public async Task<IActionResult> Photos(int albumId) 
        {
            var photos = await _typicodeService.GetPhotosAsync(albumId);
            var photosViewModel = _mapper.Map<List<PhotosViewModel>>(photos);
            return View(photosViewModel);
        }

        [Route("Photos/{photoId}/Comments")]
        public async Task<IActionResult> Comments(int photoId) 
        {
            var comments = await _typicodeService.GetCommentsAsync(photoId);
            var commentsViewModel = _mapper.Map<List<CommentViewModel>>(comments);
            return PartialView("_PhotoComments", commentsViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
