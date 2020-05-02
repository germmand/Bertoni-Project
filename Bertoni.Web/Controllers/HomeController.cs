using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bertoni.Web.Models;
using Bertoni.Core.Services;

namespace Bertoni.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITypicodeService _typicodeService;

        public HomeController(ILogger<HomeController> logger,
                              ITypicodeService typicodeService)
        {
            _logger = logger;
            _typicodeService = typicodeService;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _typicodeService.GetAlbumsAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
